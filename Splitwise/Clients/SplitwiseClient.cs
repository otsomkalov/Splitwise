using System.Net.Http;
using RestSharp;
using Splitwise.Clients.Interfaces;

namespace Splitwise.Clients
{
    public class SplitwiseClient : AnonymousSplitwiseClient, ISplitwiseClient
    {
        public IUserClient User { get; }

        public IFriendClient Friend { get; }

        public IExpenseClient Expense { get; }

        public SplitwiseClient(string apiKey) : base(apiKey)
        {
            User = new UserClient(RestClient);
            Friend = new FriendClient(RestClient);
            Expense = new ExpenseClient(RestClient);
        }
    }
}