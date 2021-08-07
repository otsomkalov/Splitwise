using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Splitwise.Requests.Expense
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CreateExpenseRequest
    {
        public double Cost { get; set; }

        public int GroupId { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public DateTime Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RepeatInterval RepeatInterval { get; set; }

        public string CurrencyCode { get; set; }

        public int CategoryId { get; set; } = 0;

        public bool? SplitEqually { get; set; }

        [JsonIgnore]
        public IList<PaymentRequest> Payments { get; set; }
    }
}
