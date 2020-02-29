using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.DAL.Interface
{
    public interface ICommentRepository
    {
        List<QuestionComment> GetCommentByQuestionId(int questionId);
        List<AnswerComment> GetCommentByAnswerId(int answerId);
        List<AnswerComment> CommentForAnswer(int userId, int answerId, string commentContent);
        List<QuestionComment> CommentForQuestion(int userId, int questionId, string commentContent);
    }
}
