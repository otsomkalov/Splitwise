using RestSharp;
using RestSharp.Serializers.Json;
using RichardSzalay.MockHttp;
using Splitwise.Clients;
using Splitwise.Options;

namespace Splitwise.Tests.Unit.Clients;

public class CategoryClientTests
{
    [Fact]
    public async Task ListAsync_Works()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/Category/list.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_categories")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new CategoryClient(restClient);

        // Act

        var categories = await client.ListAsync();

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.Single(categories);
        Assert.Contains(categories, category => category.IconTypes is not null);
    }
}