using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Splitwise.Requests.Expense
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UpsertExpenseRequest
    {
        public decimal Cost { get; init; }

        public int GroupId { get; init; }

        public string Description { get; init; }

        public string Details { get; init; }

        public DateTime Date { get; init; }

        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public RepeatInterval RepeatInterval { get; init; }

        public string CurrencyCode { get; init; }

        public int CategoryId { get; init; } = 0;

        public bool? SplitEqually { get; init; }

        [JsonIgnore]
        public IList<BasePaymentRequest> Payments { get; init; }
    }
}