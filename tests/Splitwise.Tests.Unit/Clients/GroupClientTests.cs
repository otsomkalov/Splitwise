using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.Json;
using RichardSzalay.MockHttp;
using Splitwise.Clients;
using Splitwise.Options;
using Splitwise.Requests.Group;
using Xunit;

namespace Splitwise.Tests.Unit.Clients;

public class GroupClientTests
{
    private const string BaseUrl = "https://secure.splitwise.com/api/v3.0";

    [Fact]
    public async Task ListAsync_ReturnsGroups_OnSuccessfulRequest()
    {
        // Arrange
        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = System.IO.File.OpenRead("Mocks/Group/list.json");

        var request = messageHandler.When(HttpMethod.Get, $"{BaseUrl}/get_groups")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new(BaseUrl));

        var client = new GroupClient(restClient);

        // Act
        var response = await client.ListAsync();

        // Assert
        Assert.Equal(1, messageHandler.GetMatchCount(request));
        Assert.Single(response.Groups);
        var group = Assert.Single(response.Groups);
        Assert.Equal(123, group.Id);
        Assert.Equal("Test Group", group.Name);
    }

    [Fact]
    public async Task GetAsync_ReturnsGroup_OnSuccessfulRequest()
    {
        // Arrange
        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = System.IO.File.OpenRead("Mocks/Group/details.json");

        var request = messageHandler.When(HttpMethod.Get, $"{BaseUrl}/get_group/123")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new(BaseUrl));

        var client = new GroupClient(restClient);

        // Act
        var group = await client.GetAsync(123);

        // Assert
        Assert.Equal(1, messageHandler.GetMatchCount(request));
        Assert.NotNull(group);
        Assert.Equal(123, group.Id);
        Assert.Equal("Test Group", group.Name);
    }

    [Fact]
    public async Task CreateAsync_ReturnsGroup_OnSuccessfulRequest()
    {
        // Arrange
        var messageHandler = new MockHttpMessageHandler();

        await using var responseContent = System.IO.File.OpenRead("Mocks/Group/details.json");

        var request = messageHandler.When(HttpMethod.Post, $"{BaseUrl}/create_group")
            .Respond("application/json", responseContent);

        var restClient = new RestClient(messageHandler,
            configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings),
            configureRestClient: options => options.BaseUrl = new(BaseUrl));

        var client = new GroupClient(restClient);
        var createGroupRequest = new CreateGroupRequest { Name = "Test Group" };

        // Act
        var result = await client.CreateAsync(createGroupRequest);

        // Assert
        Assert.Equal(1, messageHandler.GetMatchCount(request));
        Assert.True(result.IsSuccess);
        Assert.Equal(123, result.Value.Id);
        Assert.Equal("Test Group", result.Value.Name);
    }

    [Fact]
    public async Task CreateAsync_ReturnsValidationError_WhenNameIsEmpty()
    {
        // Arrange
        var restClient = new RestClient(configureRestClient: options => options.BaseUrl = new(BaseUrl));
        var client = new GroupClient(restClient);
        var createGroupRequest = new CreateGroupRequest { Name = "" };

        // Act
        var result = await client.CreateAsync(createGroupRequest);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Message == "'Name' must not be empty.");
    }
}