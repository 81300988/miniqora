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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IDbConnection _dbConnection;
        public AnswerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Answer> AddAnswer(AddAnswerRequest model)
        {
            var paramForAddAnswer = new DynamicParameters();
            paramForAddAnswer.Add("@AnswerContent", model.AnswerContent);
            paramForAddAnswer.Add("@UserId", model.UserId);
            paramForAddAnswer.Add("@QuestionId", model.QuestionId);
            paramForAddAnswer.Add("@CreatedDate", model.CreatedDate);

            var answerId = _dbConnection.Query<int>(@"insert into Answer(AnswerContent, UserId, QuestionId, CreatedDate) 
                                                        values(@AnswerContent,@UserId,@QuestionId,@CreatedDate);
                                                    SELECT CAST(SCOPE_IDENTITY() as int)", paramForAddAnswer).Single();

            var paramForGetAnswers = new DynamicParameters();
            paramForGetAnswers.Add("@QuestionId", model.QuestionId);

            var answers = _dbConnection.Query<Answer>(@"select	a.AnswerId,
		                                                        a.AnswerContent,
		                                                        a.UserId as UserAnswerId,
		                                                        a.CreatedDate as AnswerDate,
		                                                        u.Avatar,
		                                                        u.FirstName + ' ' + u.LastName as UserFullName,
		                                                        (select count(AnswerId) from Vote as v where v.AnswerId=a.AnswerId) as TotalVote
                                                        from	Answer as a
                                                        join	[User] as u on a.UserId=u.UserId
                                                        where	QuestionId = @QuestionId 
                                                        order by AnswerDate", paramForGetAnswers);

            return answers;
        }
        public int UpdateVoteForAnswer(UpdateVoteForAnswer model)
        {
            var paramForUpdateVote = new DynamicParameters();
            paramForUpdateVote.Add("@AnswerId", model.AnswerId);
            paramForUpdateVote.Add("@UserId", model.UserId);

            var totalVote = _dbConnection.QueryFirstOrDefault<int>(@"if not exists(select 1 from vote where UserId = @UserId and AnswerId = @AnswerId)
                                                                    begin

                                                                        insert into Vote(UserId, AnswerId, CreatedDate)
                                                                        values(@UserId, @AnswerId, getdate())
                                                                    end
                                                                    else
                                                                    begin
                                                                        delete Vote where  UserId = @UserId and AnswerId = @AnswerId
                                                                    end
                                                                    select count(VoteId) from Vote where AnswerId = @AnswerId", paramForUpdateVote);
            return totalVote;
        }
    }
}
