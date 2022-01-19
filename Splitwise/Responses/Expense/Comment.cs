using System;
using Splitwise.Responses.User;

namespace Splitwise.Responses.Expense
{
    public record Comment(
        int Id,
        string Content,
        string CommentType,
        string RelationType,
        int RelationId,
        DateTime CreatedAt,
        DateTime? DeletedAt,
        BaseUser User
    );
}