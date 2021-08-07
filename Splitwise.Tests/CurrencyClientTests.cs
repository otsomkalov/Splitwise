using System.Threading.Tasks;
using Splitwise.Clients;
using Xunit;
using FluentAssertions;

namespace Splitwise.Tests
{
    public class CurrencyClientTests
    {
        [Fact]
        public async Task ListAsync_ShouldWork()
        {
            // Arrange

            var anonymousSplitwiseClient = new AnonymousSplitwiseClient();

            // Act

            var currenciesResponse = await anonymousSplitwiseClient.Currency.ListAsync();

            // Assert

            currenciesResponse.Should().NotBeNull();
            currenciesResponse.Errors.Should().BeNull();
            currenciesResponse.Currencies.Should().NotBeNullOrEmpty();
        }
    }
}