using System;
using Splitwise.Responses.Shared;

namespace Splitwise.Responses.User
{
    public class CurrentUserResponse : BasePersonResponse
    {
        public string ForceRefreshAt { get; set; }

        public string Locale { get; set; }

        public string CountryCode { get; set; }

        public string DateFormat { get; set; }

        public string DefaultCurrency { get; set; }

        public string DefaultGroupId { get; set; }

        public DateTime NotificationsRead { get; set; }

        public int NotificationsCount { get; set; }

        public NotificationsSettingsResponse Notifications { get; set; }
    }
}
