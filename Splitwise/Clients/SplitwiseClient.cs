// using System;
// using RestSharp;
// using RestSharp.Serializers.NewtonsoftJson;
// using Splitwise.Clients.Interfaces;
// using Splitwise.Options;
//
// namespace Splitwise.Clients
// {
//     public class SplitwiseClient : AnonymousSplitwiseClient, ISplitwiseClient
//     {
//         public const string SplitwiseUrl = "https://secure.splitwise.com/api/v3.0/";
//
//         // public IUserClient User { get; }
//         // public IExpenseClient Expense { get; }
//
//         // public SplitwiseClient(string apiKey)
//         // {
//         //     if (string.IsNullOrEmpty(apiKey))
//         //     {
//         //         throw new ArgumentNullException(nameof(apiKey));
//         //     }
//         //
//         //     RestClient = RestClient
//         //         .AddDefaultHeader("Authorization", $"Bearer {apiKey}");
//         //
//         //     User = new UserClient(apiKey);
//         //     Expense = new ExpenseClient(apiKey);
//         //
//         // }
//     }
// }
