using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FluentResults;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Errors;
using Splitwise.Options;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Expense;
using Splitwise.Validators.Expense;

namespace Splitwise.Clients
{
    internal class ExpenseClient : IExpenseClient
    {
        private const string PaidFieldFormat = "users__{0}__paid_share";
        private const string OwedFieldFormat = "users__{0}__owed_share";

        private const string ExistingUserIdFieldFormat = "users__{0}__user_id";

        private const string NewUserFirstNameFieldFormat = "users__{0}__first_name";
        private const string NewUserLastNameFieldFormat = "users__{0}__last_name";
        private const string NewUserEmailFieldFormat = "users__{0}__email";

        private readonly IRestClient _restClient;

        public ExpenseClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Result<FullExpense>> GetAsync(long id)
        {
            var request = new RestRequest("get_expense/{id}")
                .AddUrlSegment("id", id);

            var response = await _restClient.ExecuteGetAsync<GetExpenseResponse>(request);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Result.Ok(response.Data.Expense),
                HttpStatusCode.Forbidden => Result.Fail(new ForbiddenError(response.Data.Errors.Base.First())),
                _ => Result.Fail(new UnknownError(response.Content))
            };
        }

        public async Task<IReadOnlyCollection<Expense>> ListAsync()
        {
            var request = new RestRequest("get_expenses");

            var response = await _restClient.GetAsync<ListExpensesResponse>(request);

            return response.Expenses;
        }

        public async Task<Result<Expense>> CreateAsync(CreateEqualSplitExpenseRequest request)
        {
            var validationResult = await ValidateRequestAsync(request);

            if (validationResult.IsFailed)
            {
                return validationResult;
            }

            var restRequest = new RestRequest("create_expense")
                .AddJsonBody(request);

            var (expenses, errors) = await _restClient.PostAsync<UpsertExpenseResponse>(restRequest);

            if (errors.Base == null)
            {
                return Result.Ok(expenses.First());
            }

            var result = new Result();

            result.WithErrors(errors.Base);

            return result;
        }

        public async Task<Result<Expense>> CreateAsync(CreateSharesSplitExpenseRequest request)
        {
            var getRequestBodyResult = await GetUpsertRequestBodyAsync(request);

            if (getRequestBodyResult.IsFailed)
            {
                return getRequestBodyResult.ToResult();
            }

            var restRequest = new RestRequest("create_expense")
                .AddJsonBody(getRequestBodyResult.Value);

            var (expenses, errors) = await _restClient.PostAsync<UpsertExpenseResponse>(restRequest);

            if (errors.Base == null)
            {
                return Result.Ok(expenses.First());
            }

            var result = new Result();

            result.WithErrors(errors.Base);

            return result;
        }

        public async Task<Result<FullExpense>> CreateFromSentenceAsync(CreateExpenseFromSentenceRequest request)
        {
            var restRequest = new RestRequest("parse_sentence")
                .AddJsonBody(request);

            var response = await _restClient.ExecutePostAsync<CreateExpenseFromSentenceResponse>(restRequest);

            if (!response.Data.Valid)
            {
                var result = new Result();

                if (response.Data.Expense.Errors.Cost.Any())
                {
                    result = result.WithError(new ParsingError("cost", response.Data.Expense.Errors.Cost));
                }

                if (response.Data.Expense.Errors.Shares.Any())
                {
                    result = result.WithError(new ParsingError("shares", response.Data.Expense.Errors.Cost));
                }

                if (response.Data.Expense.Errors.Base.Any())
                {
                    result = result.WithError(new ParsingError("base", response.Data.Expense.Errors.Cost));
                }

                return result;
            }

            return Result.Ok<FullExpense>(response.Data.Expense);
        }

        public async Task<Result<Expense>> UpdateAsync(long id, UpdateSharesSplitExpenseRequest request)
        {
            var getRequestBodyResult = await GetUpsertRequestBodyAsync(request);

            if (getRequestBodyResult.IsFailed)
            {
                return getRequestBodyResult.ToResult();
            }

            var restRequest = new RestRequest("update_expense/{id}")
                .AddUrlSegment("id", id)
                .AddJsonBody(getRequestBodyResult.Value);

            var (expenses, errors) = await _restClient.PostAsync<UpsertExpenseResponse>(restRequest);

            if (errors.Base == null)
            {
                return Result.Ok(expenses.First());
            }

            var result = new Result();

            result.WithErrors(errors.Base);

            return result;
        }

        public async Task<Result> DeleteAsync(long id)
        {
            var request = new RestRequest("delete_expense/{id}")
                .AddUrlSegment("id", id);

            var (_, errors) = await _restClient.PostAsync<DeleteAndRestoreResponse>(request);

            if (errors?.Base == null)
            {
                return Result.Ok();
            }

            var result = new Result();

            result.WithErrors(errors.Base);

            return result;
        }

        public async Task<Result> RestoreAsync(long id)
        {
            var request = new RestRequest("undelete_expense/{id}")
                .AddUrlSegment("id", id);

            var (_, errors) = await _restClient.PostAsync<DeleteAndRestoreResponse>(request);

            if (errors?.Base == null)
            {
                return Result.Ok();
            }

            var result = new Result();

            result.WithErrors(errors.Base);

            return result;
        }

        private static async Task<Result<JsonNode>> GetUpsertRequestBodyAsync(BaseSharesSplitExpenseRequest request)
        {
            var result = new Result<JsonNode>();

            var validator = new BaseExpenseRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    result.WithError(new ValidationError(error.ErrorMessage));
                }

                return result;
            }

            var requestBody = JsonSerializer.SerializeToNode(request, JsonOptions.JsonSerializerSettings)!;

            var paymentsRequests = request.Payments.ToArray();

            for (var i = 0; i < paymentsRequests.Length; i++)
            {
                var payment = paymentsRequests[i];

                var paidFieldName = string.Format(PaidFieldFormat, i);
                var owedFieldName = string.Format(OwedFieldFormat, i);

                requestBody[paidFieldName] = payment.PaidShare.ToString(CultureInfo.InvariantCulture);
                requestBody[owedFieldName] = payment.OwedShare.ToString(CultureInfo.InvariantCulture);

                if (payment is ExistingUserPayment existingUserPayment)
                {
                    var userIdPropName = string.Format(ExistingUserIdFieldFormat, i);

                    requestBody[userIdPropName] = existingUserPayment.UserId;
                }

                if (payment is NewUserPayment newUserPayment)
                {
                    var firstNamePropName = string.Format(NewUserFirstNameFieldFormat, i);
                    var lastNamePropName = string.Format(NewUserLastNameFieldFormat, i);
                    var emailPropName = string.Format(NewUserEmailFieldFormat, i);

                    requestBody[firstNamePropName] = newUserPayment.FirstName;
                    requestBody[lastNamePropName] = newUserPayment.LastName;
                    requestBody[emailPropName] = newUserPayment.Email;
                }
            }

            return result.WithValue(requestBody);
        }

        private static async Task<Result> ValidateRequestAsync(BaseExpenseRequest request)
        {
            var result = new Result();

            var validator = new BaseExpenseRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    result.WithError(new ValidationError(error.ErrorMessage));
                }

                return result;
            }

            return Result.Ok();
        }
    }
}