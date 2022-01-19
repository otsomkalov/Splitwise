namespace Splitwise.Responses.User
{
    public class NotificationsSettingsResponse
    {
        public bool AddedAsFriend { get; init; }

        public bool AddedToGroup { get; init; }

        public bool ExpenseAdded { get; init; }

        public bool ExpenseUpdated { get; init; }

        public bool Bills { get; init; }

        public bool Payments { get; init; }

        public bool Announcements { get; init; }
    }
}