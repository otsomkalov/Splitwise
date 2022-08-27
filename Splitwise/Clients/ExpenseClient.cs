using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using FluentResults;
using Newtonsoft.Json;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Errors;
using Splitwise.Extensions;
using Splitwise.Options;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Expense;
using Splitwise.Validators.Expense;

namespace Splitwise.Clients
{
    internal class ExpenseClient : IExpenseClient
    {
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

            return Result.Ok(response.Data.Expense as FullExpense);
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

        private static async Task<Result<Dictionary<string, string>>> GetUpsertRequestBodyAsync(BaseSharesSplitExpenseRequest request)
        {
            var result = new Result<Dictionary<string, string>>();

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

            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                JsonConvert.SerializeObject(request, JsonOptions.JsonSerializerSettings),
                JsonOptions.JsonSerializerSettings);

            var paymentsRequests = request.Payments.ToArray();

            for (var i = 0; i < paymentsRequests.Length; i++)
            {
                var payment = paymentsRequests[i];

                if (payment is ExistingUserPayment existingUserPayment)
                {
                    requestBody.Add(GetPaymentRequestBody(existingUserPayment, i));
                }

                if (payment is NewUserPayment newUserPayment)
                {
                    requestBody.Add(GetPaymentRequestBody(newUserPayment, i));
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

        private static IDictionary<string, string> GetPaymentRequestBody<T>(T payment, int paymentIndex)
        {
            var requestBody = new Dictionary<string, string>();

            foreach (var property in typeof(T).GetProperties())
            {
                var jsonPropertyAttribute = property.GetCustomAttribute<JsonPropertyAttribute>();

                if (jsonPropertyAttribute == null || string.IsNullOrEmpty(jsonPropertyAttribute.PropertyName))
                {
                    continue;
                }

                var propertyValue = property.GetValue(payment);

                if (propertyValue != null)
                {
                    requestBody.Add(string.Format(jsonPropertyAttribute.PropertyName, paymentIndex), propertyValue.ToString());
                }
            }

            return requestBody;
        }
    }
}