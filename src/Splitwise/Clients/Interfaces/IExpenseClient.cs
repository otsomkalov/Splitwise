using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Expense;
using Splitwise.Responses.Expense;

namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Defines expense operations in Splitwise.
    /// </summary>
    public interface IExpenseClient
    {
        /// <summary>
        /// Retrieves a full expense by identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <returns>A result containing the expense when found.</returns>
        Task<Result<FullExpense>> GetAsync(long id);

        /// <summary>
        /// Lists expenses visible to the authenticated user.
        /// </summary>
        /// <returns>A read-only collection of expenses.</returns>
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
        /// <param name="request">Equal-split expense creation payload.</param>
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
        /// <param name="request">Share-split expense creation payload.</param>
        Task<Result<Expense>> CreateAsync(CreateSharesSplitExpenseRequest request);

        /// <summary>
        /// Creates an expense from a natural-language sentence via Splitwise smart parsing.
        /// </summary>
        /// <param name="request">Sentence-based expense creation payload.</param>
        /// <returns>A result containing the created full expense on success.</returns>
        Task<Result<FullExpense>> CreateFromSentenceAsync(CreateExpenseFromSentenceRequest request);

        /// <summary>
        /// Updates an existing expense using share-based split values.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <param name="request">Expense update payload.</param>
        /// <returns>A result containing the updated expense on success.</returns>
        Task<Result<Expense>> UpdateAsync(long id, UpdateSharesSplitExpenseRequest request);

        /// <summary>
        /// Deletes an expense by identifier.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <returns>A result indicating whether deletion succeeded.</returns>
        Task<Result> DeleteAsync(long id);

        /// <summary>
        /// Restores a previously deleted expense.
        /// </summary>
        /// <param name="id">Expense identifier.</param>
        /// <returns>A result indicating whether restore succeeded.</returns>
        Task<Result> RestoreAsync(long id);
    }
}