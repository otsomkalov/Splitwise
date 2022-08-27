using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Splitwise.Clients.Interfaces;
using Splitwise.Errors;
using Splitwise.Requests.Expense;
using Splitwise.Tests.Settings;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Splitwise.Tests.Clients;

public class ExpenseClientTests
{
    private readonly ISplitwiseClient _client;
    private readonly ExpenseClientTestsSettings _settings;

    public ExpenseClientTests(ISplitwiseClient client, IOptions<ExpenseClientTestsSettings> settings)
    {
        _client = client;
        _settings = settings.Value;
    }

    [Fact]
    public async Task ListAsync_Works()
    {
        // Act

        var expenses = await _client.Expense.ListAsync();

        // Assert

        expenses.Should().NotBeNullOrEmpty();
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
    }

    [Fact]
    public async Task GetAsync_ReturnsNotFoundError_ForNonExistingTransaction()
    {
        // Act

        var getExpenseResult = await _client.Expense.GetAsync(long.MaxValue);

        // Assert

        getExpenseResult.Should().NotBeNull();
        getExpenseResult.IsFailed.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_SplitEqual_Works()
    {
        // Arrange

        var createRequest = new CreateEqualSplitExpenseRequest(1, "Test", "UAH", _settings.GroupId);

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
                PaidShare = 1
            },
            new ExistingUserPayment
            {
                UserId = _settings.FriendId,
                OwedShare = 1,
                PaidShare = 0
            }
        };

        var createRequest = new CreateSharesSplitExpenseRequest(1, "test", "UAH", payments);

        // Act

        var createExpenseResult = await _client.Expense.CreateAsync(createRequest);

        // Assert

        createExpenseResult.Should().NotBeNull();
        createExpenseResult.IsSuccess.Should().BeTrue();
        createExpenseResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateFromSentenceAsync_WithCorrectInput_ReturnsCreatedExpense()
    {
        // Arrange

        var request = new CreateExpenseFromSentenceRequest("I owe Eve one UAH");

        // Act

        var parseExpenseResult = await _client.Expense.CreateFromSentenceAsync(request);

        // Assert

        parseExpenseResult.Should().NotBeNull();
        parseExpenseResult.IsSuccess.Should().BeTrue();
        parseExpenseResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateFromSentenceAsync_WithIncorrectInput_ReturnsParsingErrors()
    {
        // Arrange

        var request = new CreateExpenseFromSentenceRequest("Ass we can");

        // Act

        var parseExpenseResult = await _client.Expense.CreateFromSentenceAsync(request);

        // Assert

        parseExpenseResult.Should().NotBeNull();
        parseExpenseResult.IsFailed.Should().BeTrue();
        parseExpenseResult.Errors.Should().AllBeOfType<ParsingError>();
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
                UserId = _settings.FriendId,
                OwedShare = 0,
                PaidShare = 2
            },
            new ExistingUserPayment
            {
                UserId = _settings.CurrentUserId,
                OwedShare = 2,
                PaidShare = 0
            }
        };

        var request = new UpdateSharesSplitExpenseRequest(2, "Updated test expense", "UAH", payments);

        // Act

        var updateExpenseResult = await _client.Expense.UpdateAsync(_settings.ExistingExpenseId, request);

        // Assert

        updateExpenseResult.Should().NotBeNull();
        updateExpenseResult.IsSuccess.Should().BeTrue();
        updateExpenseResult.Value.Should().NotBeNull();
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