namespace Splitwise.Responses.User
{
    public class NotificationsSettingsResponse
    {
        public bool AddedAsFriend { get; set; }

        public bool AddedToGroup { get; set; }

        public bool ExpenseAdded { get; set; }

        public bool ExpenseUpdated { get; set; }

        public bool Bills { get; set; }

        public bool Payments { get; set; }

        public bool Announcements { get; set; }
    }
}
