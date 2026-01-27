using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Splitwise.Converters;

namespace Splitwise.Requests.Expense
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class BaseExpenseRequest
    {
        protected BaseExpenseRequest(decimal cost, string description, string currencyCode)
        {
            Cost = cost;
            Description = description;
            CurrencyCode = currencyCode;
        }

        [JsonConverter(typeof(NumberToStringConverter))]
        public decimal Cost { get; }

        public int GroupId { get; protected init; }

        public string Description { get; }

        public string Details { get; init; }

        /// <summary>
        /// Date of expense. If not set - will be equal to the current moment.
        /// </summary>
        public DateTime? Date { get; init; }

        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public RepeatInterval RepeatInterval { get; init; }

        public string CurrencyCode { get; }

        public int? CategoryId { get; init; }
    }
}