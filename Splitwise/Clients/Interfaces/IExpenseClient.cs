using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Expense;

namespace Splitwise.Clients.Interfaces
{
    public interface IExpenseClient
    {
        Task<Result<CreateExpenseResponse>> CreateAsync(CreateExpenseRequest request);
    }
}
