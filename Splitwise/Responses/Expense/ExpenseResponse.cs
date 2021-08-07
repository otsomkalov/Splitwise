using System;
using System.Collections.Generic;
using Splitwise.Models;
using Splitwise.Requests.Expense;

namespace Splitwise.Responses.Expense
{
    public class ExpenseResponse
    {
        public string Cost { get; set; }

        public long GroupId { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public DateTimeOffset Date { get; set; }

        public string RepeatInterval { get; set; }

        public string CurrencyCode { get; set; }

        public long CategoryId { get; set; }

        public long Id { get; set; }

        public long FriendshipId { get; set; }

        public long ExpenseBundleId { get; set; }

        public bool Repeats { get; set; }

        public bool EmailReminder { get; set; }

        public object EmailReminderInAdvance { get; set; }

        public string NextRepeat { get; set; }

        public long CommentsCount { get; set; }

        public bool Payment { get; set; }

        public bool TransactionConfirmed { get; set; }

        public List<object> Repayments { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Category CreatedBy { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public Category UpdatedBy { get; set; }

        public DateTimeOffset DeletedAt { get; set; }

        public Category DeletedBy { get; set; }

        public Category Category { get; set; }

        public Receipt Receipt { get; set; }

        public List<object> Users { get; set; }

        public List<object> Comments { get; set; }
    }
}
