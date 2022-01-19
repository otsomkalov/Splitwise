using System.Collections.Generic;
using System.Linq;
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

        public async Task<FullExpense> GetAsync(long id)
        {
            var request = new RestRequest("get_expense/{id}")
                .AddUrlSegment("id", id);

            var response = await _restClient.GetAsync<GetExpenseResponse>(request);

            return response.Expense;
        }

        public async Task<IReadOnlyCollection<Expense>> ListAsync()
        {
            var request = new RestRequest("get_expenses");

            var response = await _restClient.GetAsync<ListExpensesResponse>(request);

            return response.Expenses;
        }

        public async Task<Result<Expense>> CreateAsync(UpsertExpenseRequest request)
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

        public Task<CreateExpenseFromSentenceResponse> CreateFromSentenceAsync(CreateExpenseFromSentenceRequest request)
        {
            var restRequest = new RestRequest("parse_expense")
                .AddJsonBody(request);

            return _restClient.PostAsync<CreateExpenseFromSentenceResponse>(restRequest);
        }

        public async Task<Result<Expense>> UpdateAsync(int id, UpsertExpenseRequest request)
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

        public async Task<Result> DeleteAsync(int id)
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

        public async Task<Result> RestoreAsync(int id)
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

        private static async Task<Result<Dictionary<string, string>>> GetUpsertRequestBodyAsync(UpsertExpenseRequest request)
        {
            var result = new Result<Dictionary<string, string>>();

            var validator = new CreateExpenseRequestValidator();

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

            for (var i = 0; i < request.Payments.Count; i++)
            {
                var payment = request.Payments[i];

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