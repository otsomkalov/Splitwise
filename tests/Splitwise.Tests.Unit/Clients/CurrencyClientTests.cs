using RestSharp;
using RestSharp.Serializers.Json;
using RichardSzalay.MockHttp;
using Splitwise.Clients;
using Splitwise.Options;

namespace Splitwise.Tests.Unit.Clients;

public class CurrencyClientTests
{
    [Fact]
    public async Task ListAsync_Works()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/Currency/list.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_currencies")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new CurrencyClient(restClient);

        // Act

        var currencies = await client.ListAsync();

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.Single(currencies);
        Assert.Contains(currencies, currency => !string.IsNullOrEmpty(currency.CurrencyCode));
    }
}