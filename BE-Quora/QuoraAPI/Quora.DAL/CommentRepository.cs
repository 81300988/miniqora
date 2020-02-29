using Dapper;
using Quora.DAL.Interface;
using Quora.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quora.DAL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbConnection _dbConnection;
        public CommentRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<AnswerComment> CommentForAnswer(int userId, int answerId, string commentContent)
        {
            var paramForAddAnswerComment = new DynamicParameters();
            paramForAddAnswerComment.Add("@CommentContent", commentContent);
            paramForAddAnswerComment.Add("@UserId", userId);
            paramForAddAnswerComment.Add("@AnswerId", answerId);
            paramForAddAnswerComment.Add("@CreatedDate", DateTime.Now);

            _dbConnection.Execute(@"insert into AnswerComment(CommentContent, AnswerId, CreatedDate, UserId) 
                                                        values(@CommentContent, @AnswerId, @CreatedDate, @UserId)", paramForAddAnswerComment);

            List<AnswerComment> comments = new List<AnswerComment>();

            comments = _dbConnection.Query<AnswerComment>(@"select ac.AnswerCommentId, ac.CommentContent, ac.AnswerId, ac.CreatedDate, 
                                                                        u.FirstName + ' ' + u.LastName as Commenter,
                                                                        u.Avatar
                                                                    from AnswerComment as ac 
                                                                    join [User] as u on ac.UserId=u.userId
                                                                    where AnswerId = @answerId
                                                                    order by ac.CreatedDate",
                                                    new { answerId = answerId }).ToList();

            return comments;
        }

        public List<QuestionComment> CommentForQuestion(int userId, int questionId, string commentContent)
        {
            var paramForAddQuestionComment = new DynamicParameters();
            paramForAddQuestionComment.Add("@CommentContent", commentContent);
            paramForAddQuestionComment.Add("@UserId", userId);
            paramForAddQuestionComment.Add("@QuestionId", questionId);
            paramForAddQuestionComment.Add("@CreatedDate", DateTime.Now);

            _dbConnection.Execute(@"insert into QuestionComment(CommentContent, QuestionId, CreatedDate, UserId) 
                                                        values(@CommentContent, @QuestionId, @CreatedDate, @UserId)", paramForAddQuestionComment);

            List<QuestionComment> comments = new List<QuestionComment>();

            comments = _dbConnection.Query<QuestionComment>(@"select qc.QuestionCommentId, qc.CommentContent, qc.QuestionId, qc.CreatedDate, 
                                                                        u.FirstName + ' ' + u.LastName as Commenter,
                                                                        u.Avatar
                                                                    from QuestionComment as qc 
                                                                    join [User] as u on qc.UserId=u.userId
                                                                    where QuestionId = @questionId
                                                                    order by qc.CreatedDate",
                                                new { questionId = questionId }).ToList();

            return comments;
        }

        public List<QuestionComment> GetCommentByQuestionId(int questionId)
        {
            List<QuestionComment> comments = new List<QuestionComment>();

            comments = _dbConnection.Query<QuestionComment>(@"select qc.QuestionCommentId, qc.CommentContent, qc.QuestionId, qc.CreatedDate, 
                                                                        u.FirstName + ' ' + u.LastName as Commenter,
                                                                        u.Avatar
                                                                    from QuestionComment as qc 
                                                                    join [User] as u on qc.UserId=u.userId
                                                                    where QuestionId = @questionId
                                                                    order by qc.CreatedDate",
                                                    new { questionId = questionId }).ToList();

            return comments;
        }

        public List<AnswerComment> GetCommentByAnswerId(int answerId)
        {
            List<AnswerComment> comments = new List<AnswerComment>();

            comments = _dbConnection.Query<AnswerComment>(@"select ac.AnswerCommentId, ac.CommentContent, ac.AnswerId, ac.CreatedDate, 
                                                                        u.FirstName + ' ' + u.LastName as Commenter,
                                                                        u.Avatar
                                                                    from AnswerComment as ac 
                                                                    join [User] as u on ac.UserId=u.userId
                                                                    where AnswerId = @answerId
                                                                    order by ac.CreatedDate",
                                                    new { answerId = answerId }).ToList();

            return comments;
        }
    }
}
