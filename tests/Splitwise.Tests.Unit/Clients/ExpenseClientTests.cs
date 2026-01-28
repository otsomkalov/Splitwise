using RestSharp;
using RestSharp.Serializers.Json;
using RichardSzalay.MockHttp;
using Splitwise.Clients;
using Splitwise.Options;

namespace Splitwise.Tests.Unit.Clients;

public class ExpenseClientTests
{
    [Fact]
    public async Task ListAsync_ReturnsExpenses_OnSuccessfulRequest()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/Expense/list.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_expenses")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new ExpenseClient(restClient);

        // Act

        var expenses = await client.ListAsync();

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.Single(expenses);
        Assert.Contains(expenses, expense => expense.Id == 4279690811);
        Assert.Contains(expenses, expense => expense.GroupId == 37454049);
        Assert.Contains(expenses, expense => expense.Repayments.Count == 1);
        Assert.Contains(expenses, expense => expense.Users.Count == 2);
        Assert.Contains(expenses, expense => expense.Cost == 12.00);
    }

    [Fact]
    public async Task GetAsync_ReturnsExpense_OnSuccessfulRequest()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/Expense/details.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_expense/4279690811")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new ExpenseClient(restClient);

        // Act

        var getExpenseResult = await client.GetAsync(4279690811);

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.True(getExpenseResult.IsSuccess);
        Assert.NotNull(getExpenseResult.Value);

        var expense = getExpenseResult.Value;

        Assert.Equal(4279690811, expense.Id);
        Assert.Equal(37454049, expense.GroupId);
        Assert.Equal(12.00, expense.Cost);
        Assert.Equal(2, expense.Users.Count);
        Assert.Single(expense.Repayments);
    }
}