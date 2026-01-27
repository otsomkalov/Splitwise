using System;

namespace Splitwise.Requests.Expense
{
    public abstract class BaseExpenseRequest
    {
        protected BaseExpenseRequest(decimal cost, string description, string currencyCode)
        {
            Cost = cost;
            Description = description;
            CurrencyCode = currencyCode;
        }

        public decimal Cost { get; }

        public int GroupId { get; protected init; }

        public string Description { get; }

        public string Details { get; init; }

        /// <summary>
        /// Date of expense. If not set - will be equal to the current moment.
        /// </summary>
        public DateTime? Date { get; init; }

        public RepeatInterval RepeatInterval { get; init; }

        public string CurrencyCode { get; }

        public int? CategoryId { get; init; }
    }
}