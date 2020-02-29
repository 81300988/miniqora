using Dapper;
using Quora.DAL.Interface;
using Quora.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Quora.DAL
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbConnection _dbConnection;
        public QuestionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Question> GetQuestion(int currentUserId, int pageSize, int pageIndex, out int totalQuestion)
        {
            var p = new DynamicParameters();
            p.Add("@userId", currentUserId);
            p.Add("@PageSize", pageSize);
            p.Add("@Index", pageIndex);
            var questions = _dbConnection.Query<Question>("sp_GetQuestion", p, commandType: CommandType.StoredProcedure);

            totalQuestion = _dbConnection.Query<int>("select count(QuestionId) from Question").Single();

            return questions;
        }

        public List<Question> GetQuestionsByCategory(int catogoryId)
        {
            List<Question> questions = new List<Question>();
            questions = _dbConnection.Query<Question>(@"select		q.QuestionId, q.QuestionTitle, q.QuestionContent, q.CreatedDate,q.UserId, 
                                                                    c.CategoryName, c.ImageName
                                                        FROM        Question as q
                                                        inner join  QuestionCategory as qc on q.QuestionId = qc.QuestionId
                                                        inner join  Category as c on qc.CategoryId = c.CategoryId
                                                        where c.CategoryId = @categoryId", new { categoryId = catogoryId }).ToList();

            return questions;
        }

        public int AddQuestion(AddQuestionRequest model)
        {
            var paramForQuestion = new DynamicParameters();
            paramForQuestion.Add("@QuestionTitle", model.QuestionTitle);
            paramForQuestion.Add("@QuestionContent", model.QuestionContent);
            paramForQuestion.Add("@CreatedDate", model.CreatedDate);
            paramForQuestion.Add("@UserId", model.UserId);

            var questionId = _dbConnection.Query<int>(@"insert into Question(QuestionTitle, QuestionContent, CreatedDate, UserId) 
                                                        values(@QuestionTitle,@QuestionContent,@CreatedDate,@UserId);
                                                    SELECT CAST(SCOPE_IDENTITY() as int)", paramForQuestion).Single();

            foreach (var categoryId in model.CategoriesId)
            {
                var paramForQuestionCategory = new DynamicParameters();
                paramForQuestionCategory.Add("@QuestionId", questionId);
                paramForQuestionCategory.Add("@CategoryId", categoryId);

                _dbConnection.Execute(@"insert into QuestionCategory(QuestionId,CategoryId) 
                                                values(@QuestionId,@CategoryId)", paramForQuestionCategory);
            }

            return questionId;
        }
        public QuestionDetail GetQuestionDetail(int questionId)
        {
            List<QuestionComment> questionComments = new List<QuestionComment>();
            List<AnswerForAQuestion> answersForAQuestions = new List<AnswerForAQuestion>();
            QuestionDetail tempQuestionDetails = new QuestionDetail();
            QuestionDetail questionDetails = new QuestionDetail();
            answersForAQuestions = _dbConnection.Query<AnswerForAQuestion>(@"select a.AnswerId,
                                                                                    a.AnswerContent,
                                                                                    (select count(1) from Vote as v1 where v1.AnswerId=a.AnswerId and v1.UserId=1) as IsVotedForAnswer,
	                                                                                (select count(v.VoteId) from Vote as v where v.AnswerId = a.AnswerId) as TotalVote,
	                                                                                a.CreatedDate,
	                                                                                u.FirstName + ' ' + u.LastName as UserFullName,
                                                                                    u.Avatar
                                                                             from   Answer as a
                                                                             inner join [User] as u on u.UserId = a.UserId
                                                                             where a.QuestionId = @questionId
                                                                            order by a.CreatedDate", new { questionId = questionId }).ToList();

            var answerComments = _dbConnection.Query<AnswerComment>(@"select ac.AnswerCommentId,
	                                                                       ac.AnswerId,
	                                                                       ac.CommentContent,
	                                                                       ac.UserId,
	                                                                       ac.CreatedDate,
                                                                            u.Avatar
                                                                      from   AnswerComment as ac
                                                                      inner  join Answer as a on ac.AnswerId = a.AnswerId
                                                                      inner  join [User] as u on ac.UserId = u.userId
                                                                      where a.QuestionId = @questionId", new { questionId = questionId });

            foreach (var answer in answersForAQuestions)
            {
                answer.AnswerComments = answerComments.Where(x => x.AnswerId == answer.AnswerId).ToList();
            }

            questionComments = _dbConnection.Query<QuestionComment>(@"select  qc.QuestionCommentId,
		                                                                      qc.CommentContent,
		                                                                      qc.QuestionId,
		                                                                      qc.CreatedDate,
		                                                                      u.FirstName + ' ' + u.LastName as Commenter,
                                                                              u.Avatar
                                                                        from QuestionComment as qc
                                                                        inner join [User] as u on u.UserId = qc.UserId
                                                                        where qc.QuestionId = @questionId", new { questionId = questionId }).ToList();

            tempQuestionDetails = _dbConnection.QueryFirstOrDefault<QuestionDetail>(@"select q.QuestionId,
	                                                                                     q.QuestionTitle,
	                                                                                     q.QuestionContent,
	                                                                                     u.FirstName + ' ' + u.LastName as QuestionPotster
                                                                                  from Question as q
                                                                                  inner join [User] as u on u.UserId = q.UserId
                                                                                  where q.QuestionId = @questionId", new { questionId = questionId });
            if (tempQuestionDetails != null)
            {

                questionDetails.QuestionId = tempQuestionDetails.QuestionId;
                questionDetails.QuestionContent = tempQuestionDetails.QuestionContent;
                questionDetails.QuestionPotster = tempQuestionDetails.QuestionPotster;
                questionDetails.QuestionTitle = tempQuestionDetails.QuestionTitle;
            }

            questionDetails.Answers = answersForAQuestions;
            questionDetails.Comments = questionComments;
            return questionDetails;
        }
    }
}
