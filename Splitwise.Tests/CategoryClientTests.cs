using System.Threading.Tasks;
using FluentAssertions;
using Splitwise.Clients;
using Xunit;

namespace Splitwise.Tests
{
    public class CategoryClientTests
    {
        [Fact]
        public async Task ListAsync_ShouldWork()
        {
            // Arrange

            var anonymousSplitwiseClient = new AnonymousSplitwiseClient();

            // Act

            var categories = await anonymousSplitwiseClient.Category.ListAsync();

            // Assert

            categories.Should().NotBeNullOrEmpty();
        }
    }
}