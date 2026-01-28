using RestSharp;
using RestSharp.Serializers.Json;
using RichardSzalay.MockHttp;
using Splitwise.Clients;
using Splitwise.Options;
using Splitwise.Requests.User;

namespace Splitwise.Tests.Unit.Clients;

public class UserClientTests
{
    [Fact]
    public async Task GetCurrentAsync_ReturnsProfile_OnSuccessfulRequest()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/User/current.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_current_user")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new UserClient(restClient);

        // Act

        var currentUser = await client.GetCurrentAsync();

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.NotNull(currentUser);

        Assert.Equal(56856765, currentUser.Id);
        Assert.Equal("Alice", currentUser.FirstName);
        Assert.Equal("gjjtkksqqoeqtaisgn@nvhrw.com", currentUser.Email);
        Assert.Equal(-1, currentUser.DefaultGroupId);
        Assert.Equal(0, currentUser.NotificationsCount);
    }

    [Fact]
    public async Task GetAsync_ReturnsProfile_OnSuccessfulRequest()
    {
        // Arrange

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/User/details.json");

        var request = messageHandler.When(HttpMethod.Get, "https://secure.splitwise.com/api/v3.0/get_user/56856793")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new UserClient(restClient);

        // Act

        var user = await client.GetAsync(56856793);

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.NotNull(user);

        Assert.Equal(56856793, user.Id);
        Assert.Equal("Bob", user.FirstName);
        Assert.Equal("xewib49563@otodir.com", user.Email);
        Assert.False(user.CustomPicture);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsUpdatedUser_OnSuccessfulRequest()
    {
        // Arrange

        const string firstName = "Alice";
        const string lastName = "Smith";
        const string email = "gjjtkksqqoeqtaisgn@nvhrw.com";
        const string locale = "en";
        const string defaultCurrency = "USD";
        const string password = "new_password";

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/User/current.json");

        var request = messageHandler.When(HttpMethod.Post, "https://secure.splitwise.com/api/v3.0/update_user/56856793")
            .WithJsonContent<UpdateUserRequest>(req =>
                    req is
                    {
                        FirstName: firstName,
                        LastName: lastName,
                        Email: email,
                        Locale: locale,
                        DefaultCurrency: defaultCurrency,
                        Password: password
                    }
                , JsonOptions.JsonSerializerSettings)
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new UserClient(restClient);

        // Act

        var updateResult = await client.UpdateAsync(56856793, new()
        {
            FirstName = firstName,
            LastName = lastName,
            DefaultCurrency = defaultCurrency,
            Email = email,
            Locale = locale,
            Password = password
        });

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.True(updateResult.IsSuccess);

        Assert.NotNull(updateResult.Value);

        Assert.Equal(firstName, updateResult.Value.FirstName);
        Assert.Equal(lastName, updateResult.Value.LastName);
        Assert.Equal(email, updateResult.Value.Email);
        Assert.Equal(locale, updateResult.Value.Locale);
        Assert.Equal(defaultCurrency, updateResult.Value.DefaultCurrency);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsErrorDetails_OnFailedRequest()
    {
        // Arrange

        const string firstName = "Alice";

        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = File.OpenRead("Mocks/User/update-error.json");

        var request = messageHandler.When(HttpMethod.Post, "https://secure.splitwise.com/api/v3.0/update_user/56856793")
            .WithJsonContent<UpdateUserRequest>(req =>
                    req is
                    {
                        FirstName: firstName
                    }
                , JsonOptions.JsonSerializerSettings)
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new("https://secure.splitwise.com/api/v3.0"));

        var client = new UserClient(restClient);

        // Act

        var updateResult = await client.UpdateAsync(56856793, new()
        {
            FirstName = firstName
        });

        // Assert

        Assert.Equal(1, messageHandler.GetMatchCount(request));

        Assert.False(updateResult.IsSuccess);

        Assert.Single(updateResult.Errors);

        Assert.Contains(updateResult.Errors,
            error => error.Message == "Invalid API Request: you do not have permission to perform that action");
    }
}