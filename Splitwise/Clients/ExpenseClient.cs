using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using FluentResults;
using Newtonsoft.Json;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Errors;
using Splitwise.Options;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Expense;
using Splitwise.Validators.Expense;

namespace Splitwise.Clients
{
    public class ExpenseClient : BaseClient, IExpenseClient
    {
        public ExpenseClient(string apiKey) : base(apiKey)
        {
        }

        public async Task<Result<CreateExpenseResponse>> CreateAsync(CreateExpenseRequest request)
        {
            var result = Result.Ok();
            
            var validator = new CreateExpenseRequestValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    result.WithError(new ValidationError(error.PropertyName, error.ErrorMessage));
                }

                return result;
            }

            var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                JsonConvert.SerializeObject(request, JsonOptions.JsonSerializerSettings),
                JsonOptions.JsonSerializerSettings);

            for (var i = 0; i < request.Payments.Count; i++)
            {
                var payment = request.Payments[i];

                foreach (var property in typeof(PaymentRequest).GetProperties())
                {
                    var jsonPropertyAttribute = property.GetCustomAttribute<JsonPropertyAttribute>();

                    var propertyValue = property.GetValue(payment);

                    if (propertyValue != null)
                    {
                        requestBody.Add(string.Format(jsonPropertyAttribute.PropertyName, i), propertyValue.ToString());
                    }
                }
            }

            var restRequest = new RestRequest("create_expense")
                .AddJsonBody(requestBody);

            var rb = JsonConvert.SerializeObject(requestBody);
            
            return Result.Ok(await RestClient.PostAsync<CreateExpenseResponse>(restRequest));
        }
    }
}
