using System.Threading.Tasks;
using Splitwise.Clients;
using Xunit;
using FluentAssertions;

namespace Splitwise.Tests.Clients;

public class CategoryClientTests
{
    [Fact]
    public async Task ListAsync_Works()
    {
        var anonymousSplitwiseClient = new AnonymousSplitwiseClient();

        // Act

        var categories = await anonymousSplitwiseClient.Category.ListAsync();

        // Assert

        categories.Should().NotBeNullOrEmpty();
    }
}