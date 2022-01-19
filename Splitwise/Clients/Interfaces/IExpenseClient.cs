using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Expense;

namespace Splitwise.Clients.Interfaces
{
    public interface IExpenseClient
    {
        Task<FullExpense> GetAsync(long id);

        Task<IReadOnlyCollection<Expense>> ListAsync();

        Task<Result<Expense>> CreateAsync(UpsertExpenseRequest request);

        Task<CreateExpenseFromSentenceResponse> CreateFromSentenceAsync(CreateExpenseFromSentenceRequest request);

        Task<Result<Expense>> UpdateAsync(int id, UpsertExpenseRequest request);

        Task<Result> DeleteAsync(int id);

        Task<Result> RestoreAsync(int id);
    }
}