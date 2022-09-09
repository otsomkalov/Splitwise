using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Expense;

namespace Splitwise.Clients.Interfaces
{
    public interface IExpenseClient
    {
        Task<Result<FullExpense>> GetAsync(long id);

        Task<IReadOnlyCollection<Expense>> ListAsync();

        /// <summary>
        /// Creates new expense in group splitted equally between members
        /// </summary>
        /// <returns><see cref="Result"/> with created expense or one of the following errors:
        /// <list>
        ///     <item>
        ///         <see cref="Errors.ValidationError"/>s with request validation problems
        ///     </item>
        ///     <item>
        ///         <see cref="Errors.ForbiddenError"/> with info why request was forbidden
        ///     </item>
        /// </list>
        /// </returns>
        Task<Result<Expense>> CreateAsync(CreateEqualSplitExpenseRequest request);

        /// <summary>
        /// Creates new expense in group splitted by shares between members
        /// </summary>
        /// <returns><see cref="Result"/> with created expense or one of the following errors:
        /// <list>
        ///     <item>
        ///         <see cref="Errors.ValidationError"/>s with request validation problems
        ///     </item>
        ///     <item>
        ///         <see cref="Errors.ForbiddenError"/> with info why request was forbidden
        ///     </item>
        /// </list>
        /// </returns>
        Task<Result<Expense>> CreateAsync(CreateSharesSplitExpenseRequest request);

        Task<Result<FullExpense>> CreateFromSentenceAsync(CreateExpenseFromSentenceRequest request);

        Task<Result<Expense>> UpdateAsync(long id, UpdateSharesSplitExpenseRequest request);

        Task<Result> DeleteAsync(long id);

        Task<Result> RestoreAsync(long id);
    }
}