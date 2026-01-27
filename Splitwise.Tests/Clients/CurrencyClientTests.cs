using System.Threading.Tasks;
using FluentAssertions;
using Splitwise.Clients;
using Xunit;

namespace Splitwise.Tests.Clients;

public class CurrencyClientTests
{
    [Fact]
    public async Task ListAsync_Works()
    {
        // Arrange

        var anonymousSplitwiseClient = new AnonymousSplitwiseClient();

        // Act

        var currencies = await anonymousSplitwiseClient.Currency.ListAsync();

        // Assert

        currencies.Should().NotBeNullOrEmpty();
    }
}