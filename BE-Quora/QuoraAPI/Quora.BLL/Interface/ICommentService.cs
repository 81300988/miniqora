using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL.Interface
{
    public interface ICommentService
    {
        List<QuestionComment> GetCommentByQuestionId(int questionId);
        List<AnswerComment> GetCommentByAnswerId(int answerId);
        List<AnswerComment> CommentForAnswer(int userId, int answerId, string commentContent);
        List<QuestionComment> CommentForQuestion(int userId, int questionId, string commentContent);
    }
}
