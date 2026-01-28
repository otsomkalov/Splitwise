using System;

namespace Splitwise.Responses.User
{
    public record FullUser : User
    {
        public string CountryCode { get; init; }

        public string DateFormat { get; init; }

        public string DefaultCurrency { get; init; }

        public int DefaultGroupId { get; init; }

        public string ForceRefreshAt { get; init; }

        public string Locale { get; init; }

        public NotificationsSettingsResponse Notifications { get; init; }

        public int NotificationsCount { get; init; }

        public DateTime NotificationsRead { get; init; }
    }
}