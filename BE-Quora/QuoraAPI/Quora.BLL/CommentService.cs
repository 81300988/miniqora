using Quora.BLL.Interface;
using Quora.DAL.Interface;
using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public List<AnswerComment> CommentForAnswer(int userId, int answerId, string commentContent)
        {
            return _commentRepository.CommentForAnswer(userId, answerId, commentContent);
        }

        public List<QuestionComment> CommentForQuestion(int userId, int questionId, string commentContent)
        {
            return _commentRepository.CommentForQuestion(userId, questionId, commentContent);
        }

        public List<QuestionComment> GetCommentByQuestionId(int questionId)
        {
            return _commentRepository.GetCommentByQuestionId(questionId);
        }

        public List<AnswerComment> GetCommentByAnswerId(int answerId)
        {
            return _commentRepository.GetCommentByAnswerId(answerId);
        }
    }
}
