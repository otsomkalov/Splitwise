using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Splitwise.Clients.Interfaces;
using Splitwise.Errors;
using Splitwise.Requests.Expense;
using Splitwise.Tests.Integration.Settings;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Splitwise.Tests.Integration.Clients;

public class ExpenseClientTests
{
    private readonly ISplitwiseClient _client;
    private readonly ExpenseClientTestsSettings _settings;

    private readonly int _paid;

    public ExpenseClientTests(ISplitwiseClient client, IOptions<ExpenseClientTestsSettings> settings)
    {
        _client = client;
        _settings = settings.Value;

        var fixture = new Fixture();

        _paid = fixture.Create<int>() % 20;
    }

    [Fact]
    public async Task ListAsync_Works()
    {
        // Act

        var expenses = await _client.Expense.ListAsync();

        // Assert

        expenses.Should().NotBeNullOrEmpty();
        expenses.Should().Contain(e => e.Id == _settings.ExistingExpenseId);
    }

    [Fact]
    public async Task GetAsync_ReturnsExistingTransaction()
    {
        // Act

        var getExpenseResult = await _client.Expense.GetAsync(_settings.ExistingExpenseId);

        // Assert

        getExpenseResult.Should().NotBeNull();
        getExpenseResult.IsSuccess.Should().BeTrue();
        getExpenseResult.Value.Should().NotBeNull();
        getExpenseResult.Value.Id.Should().Be(_settings.ExistingExpenseId);
    }

    [Fact]
    public async Task GetAsync_ReturnsNotFoundError_ForNonExistingTransaction()
    {
        // Act

        var getExpenseResult = await _client.Expense.GetAsync(long.MaxValue);

        // Assert

        getExpenseResult.Should().NotBeNull();
        getExpenseResult.IsFailed.Should().BeTrue();
        getExpenseResult.Errors.Should().AllBeOfType<ForbiddenError>();
    }

    [Fact]
    public async Task CreateAsync_SplitEqual_Works()
    {
        // Arrange

        var createRequest = new CreateEqualSplitExpenseRequest(_paid, "Test", "UAH", _settings.GroupId);

        // Act

        var createExpenseResult = await _client.Expense.CreateAsync(createRequest);

        // Assert

        createExpenseResult.Should().NotBeNull();
        createExpenseResult.IsSuccess.Should().BeTrue();
        createExpenseResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAsync_SplitByShares_Works()
    {
        // Arrange

        var payments = new List<BasePaymentRequest>
        {
            new ExistingUserPayment
            {
                UserId = _settings.CurrentUserId,
                OwedShare = 0,
                PaidShare = _paid
            },
            new ExistingUserPayment
            {
                UserId = _settings.FriendId,
                OwedShare = _paid,
                PaidShare = 0
            }
        };

        var createRequest = new CreateSharesSplitExpenseRequest(_paid, "test", "UAH", payments);

        // Act

        var createExpenseResult = await _client.Expense.CreateAsync(createRequest);

        // Assert

        createExpenseResult.Should().NotBeNull();
        createExpenseResult.IsSuccess.Should().BeTrue();
        createExpenseResult.Value.Should().NotBeNull();
        createExpenseResult.Value.Cost.Should().Be(_paid);
        createExpenseResult.Value.Users.Should().Contain(up => up.PaidShare == _paid);
        createExpenseResult.Value.Users.Should().Contain(up => up.OwedShare == _paid);
    }

    [Fact]
    [Order(1)]
    public async Task UpdateAsync_Works()
    {
        // Arrange

        var payments = new List<BasePaymentRequest>
        {
            new ExistingUserPayment
            {
                UserId = _settings.CurrentUserId,
                OwedShare = _paid,
                PaidShare = 0
            },
            new ExistingUserPayment
            {
                UserId = _settings.FriendId,
                OwedShare = 0,
                PaidShare = _paid
            }
        };

        var request = new UpdateSharesSplitExpenseRequest(_paid, "Updated test expense", "UAH", payments);

        // Act

        var updateExpenseResult = await _client.Expense.UpdateAsync(_settings.ExistingExpenseId, request);

        // Assert

        updateExpenseResult.Should().NotBeNull();
        updateExpenseResult.IsSuccess.Should().BeTrue();
        updateExpenseResult.Value.Should().NotBeNull();
        updateExpenseResult.Value.Cost.Should().Be(_paid);
        updateExpenseResult.Value.Users.Should().Contain(up => up.PaidShare == _paid);
        updateExpenseResult.Value.Users.Should().Contain(up => up.OwedShare == _paid);
    }

    [Fact]
    [Order(2)]
    public async Task DeleteAsync_Works()
    {
        // Act

        var deleteResult = await _client.Expense.DeleteAsync(_settings.ExistingExpenseId);

        // Assert

        deleteResult.Should().NotBeNull();
        deleteResult.IsSuccess.Should().BeTrue();
    }

    [Fact]
    [Order(3)]
    public async Task RestoreAsync_Works()
    {
        // Act

        var restoreResult = await _client.Expense.RestoreAsync(_settings.ExistingExpenseId);

        // Assert

        restoreResult.Should().NotBeNull();
        restoreResult.IsSuccess.Should().BeTrue();
    }
}