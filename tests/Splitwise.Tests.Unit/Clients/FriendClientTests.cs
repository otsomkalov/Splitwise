using RestSharp;
using RestSharp.Serializers.Json;
using RichardSzalay.MockHttp;
using Splitwise.Clients;
using Splitwise.Options;

namespace Splitwise.Tests.Unit.Clients;

public class FriendClientTests
{
    [Fact]
    public async Task ListAsync_ReturnsFriends_OnSuccessfulRequest()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/Friend/list.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_friends")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new FriendClient(restClient);

        // Act

        var friends = await client.ListAsync();

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.Single(friends);
        Assert.Contains(friends, friend => friend.Id == 56856793);
        Assert.Contains(friends, friend => friend.FirstName == "Bob");
        Assert.Contains(friends, friend => friend.Email == "xewib49563@otodir.com");
        Assert.Contains(friends, friend => friend.Balance.Count == 2);
        Assert.Contains(friends, friend => friend.Groups.Count == 2);
    }

    [Fact]
    public async Task GetAsync_ReturnsFriend_OnSuccessfulRequest()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/Friend/details.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_friend/56856793")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new FriendClient(restClient);

        // Act

        var friend = await client.GetAsync(56856793);

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.NotNull(friend);

        Assert.Equal(56856793, friend.Id);
        Assert.Equal("Bob", friend.FirstName);
        Assert.Equal("xewib49563@otodir.com", friend.Email);
        Assert.Equal(2, friend.Balance.Count);
        Assert.Equal(2, friend.Groups.Count);
    }
}