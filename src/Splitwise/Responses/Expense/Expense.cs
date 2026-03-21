using System;
using System.Collections.Generic;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Category;
using Splitwise.Responses.User;

namespace Splitwise.Responses.Expense
{
    public record Expense
    {
        public BaseCategory Category { get; init; }

        public int CommentsCount { get; init; }

        public double Cost { get; init; }

        public DateTimeOffset CreatedAt { get; init; }

        public BaseUser CreatedBy { get; init; }

        public object CreationMethod { get; init; }

        public string CurrencyCode { get; init; }

        public DateTimeOffset Date { get; init; }

        public DateTimeOffset? DeletedAt { get; init; }

        public BaseUser DeletedBy { get; init; }

        public string Description { get; init; }

        public string Details { get; init; }

        public bool EmailReminder { get; init; }

        public int EmailReminderInAdvance { get; init; }

        public long? ExpenseBundleId { get; init; }

        public long? FriendshipId { get; init; }

        public long? GroupId { get; init; }

        public long Id { get; init; }

        public string NextRepeat { get; init; }

        public bool Payment { get; init; }

        public Receipt Receipt { get; init; }

        public IReadOnlyCollection<Payment> Repayments { get; init; }

        public string RepeatInterval { get; init; }

        public bool Repeats { get; init; }

        public bool TransactionConfirmed { get; init; }

        public int? TransactionId { get; init; }

        public string TransactionMethod { get; init; }

        public string TransactionStatus { get; init; }

        public DateTimeOffset UpdatedAt { get; init; }

        public BaseUser UpdatedBy { get; init; }

        public IReadOnlyCollection<UserPayment> Users { get; init; }
    }
}