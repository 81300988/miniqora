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
    public class SeedDataRepository : ISeedDataRepository
    {
        private readonly IDbConnection _dbConnection;
        public SeedDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void CreateRandomQuestions()
        {
            var users = _dbConnection.Query<User>(@"select * from [User]").ToList();
            var categories = _dbConnection.Query<Category>(@"select * from Category").ToList();

            var maxQuestionId = _dbConnection.Query<int>(@"select top 1 QuestionId from Question order by QuestionId desc").Single();

            var rndUser = new Random();

            for (int i = maxQuestionId; i < maxQuestionId + 10000; i++)
            {
                var index = rndUser.Next(0, users.Count);
                var paramForQuestion = new DynamicParameters();
                paramForQuestion.Add("@QuestionTitle", string.Format("Question Title {0}", i));
                paramForQuestion.Add("@QuestionContent", string.Format("Question Content {0}", i));
                paramForQuestion.Add("@CreatedDate", DateTime.Now);
                paramForQuestion.Add("@UserId", users[index].UserId);

                var questionId = _dbConnection.Query<int>(@"insert into Question(QuestionTitle, QuestionContent, CreatedDate, UserId) 
                                                        values(@QuestionTitle,@QuestionContent,@CreatedDate,@UserId);
                                                    SELECT CAST(SCOPE_IDENTITY() as int)", paramForQuestion).Single();

                var rndCategory = new Random();
                List<int> categorieIds = new List<int>();

                for (int j = 0; j < 3; j++)
                {
                    var indexCategory = rndCategory.Next(0, categories.Count);
                    categorieIds.Add(Convert.ToInt32(categories[indexCategory].CategoryId));
                }

                foreach (var categoryId in categorieIds.Distinct())
                {
                    var paramForQuestionCategory = new DynamicParameters();
                    paramForQuestionCategory.Add("@QuestionId", questionId);
                    paramForQuestionCategory.Add("@CategoryId", categoryId);

                    _dbConnection.Execute(@"insert into QuestionCategory(QuestionId, CategoryId) 
                                                values(@QuestionId, @CategoryId)", paramForQuestionCategory);
                }
            }
        }

        public void CreateRandomAnswer()
        {
            var users = _dbConnection.Query<User>(@"select * from [User]").ToList();
            var questions = _dbConnection.Query<Question>(@"select * from Question").ToList();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10000; i++)
            {
                var rndQuestion = new Random();
                var questionIndex = rndQuestion.Next(0, questions.Count - 1);
                var question = questions[questionIndex];

                var rndAnswer = new Random();
                var countAnswer = rndAnswer.Next(1, 10);

                for (int j = 1; j <= countAnswer; j++)
                {
                    var rndUser = new Random();
                    var index = rndUser.Next(0, users.Count);

                    //var paramForAddAnswer = new DynamicParameters();
                    //paramForAddAnswer.Add("@AnswerContent", string.Format("Answer {0} for Question {1}", j, question.QuestionId));
                    //paramForAddAnswer.Add("@UserId", users[index].UserId);
                    //paramForAddAnswer.Add("@QuestionId", question.QuestionId);
                    //paramForAddAnswer.Add("@CreatedDate", DateTime.Now);

                    //var answerId = _dbConnection.Query<int>(@"insert into Answer(AnswerContent, UserId, QuestionId, CreatedDate) 
                    //                                    values(@AnswerContent,@UserId,@QuestionId,@CreatedDate);
                    //                                SELECT CAST(SCOPE_IDENTITY() as int)", paramForAddAnswer).Single();

                    var answerContent = string.Format("Answer {0} for Question {1}", j, question.QuestionId);

                    sb.AppendFormat("insert into Answer(AnswerContent, UserId, QuestionId, CreatedDate) values('{0}',{1},{2},'{3}');",
                                    answerContent, users[index].UserId, question.QuestionId, DateTime.Now.ToString("yyyy/MM/dd"));
                }
            }

            _dbConnection.Execute(sb.ToString());
        }

        public void CreateRandomVote()
        {
            var users = _dbConnection.Query<User>(@"select * from [User]").ToList();
            var answers = _dbConnection.Query<Answer>(@"select * from Answer").ToList();

            for (int i = 0; i < 10000; i++)
            {
                StringBuilder sb = new StringBuilder();
                var rndAnswer = new Random();
                var AnswerIndex = rndAnswer.Next(0, answers.Count - 1);
                var answer = answers[AnswerIndex];

                var rndVote = new Random();
                var countVote = rndVote.Next(1, 10);

                for (int j = 1; j <= countVote; j++)
                {
                    var rndUser = new Random();
                    var index = rndUser.Next(0, users.Count);

                    //var paramForAddAnswer = new DynamicParameters();
                    //paramForAddAnswer.Add("@UserId", users[index].UserId);
                    //paramForAddAnswer.Add("@AnswerId", answer.AnswerId);
                    //paramForAddAnswer.Add("@CreatedDate", DateTime.Now);

                    //_dbConnection.Execute(@"if not exists(select 1 from vote where UserId = @UserId and AnswerId = @AnswerId)
                    //                                            begin
                    //                                             insert into Vote(UserId, AnswerId, CreatedDate) 
                    //                                                values(@UserId,@AnswerId,@CreatedDate)
                    //                                            end
                    //                                        ", paramForAddAnswer);

                    sb.AppendFormat(@"if not exists(select 1 from vote where UserId = {0} and AnswerId = {1})
                                                                begin

                                                                    insert into Vote(UserId, AnswerId, CreatedDate)
                                                                    values({0}, {1}, {2})
                                                                end; ",
                                    users[index].UserId, answer.AnswerId, DateTime.Now.ToString("yyyy/MM/dd"));
                }
                _dbConnection.Execute(sb.ToString());
            }
        }

        public void CreateRandomCommentForAnswer()
        {
            var users = _dbConnection.Query<User>(@"select * from [User]").ToList();
            var answers = _dbConnection.Query<Answer>(@"select * from Answer").ToList();

            for (int i = 0; i < 10000; i++)
            {
                StringBuilder sb = new StringBuilder();
                var rndAnswer = new Random();
                var AnswerIndex = rndAnswer.Next(0, answers.Count - 1);
                var answer = answers[AnswerIndex];

                var rndComment = new Random();
                var countComment = rndComment.Next(1, 10);

                for (int j = 1; j <= countComment; j++)
                {
                    var rndUser = new Random();
                    var index = rndUser.Next(0, users.Count);

                    var CommentContent = string.Format("{0} Comment {1} for Answer {2}", users[index].FullName, j, answer.AnswerId);

                    sb.AppendFormat(@"insert into AnswerComment(CommentContent, AnswerId, CreatedDate, UserId)
                                                                    values('{0}', {1}, '{2}', {3})", 
                                    CommentContent, answer.AnswerId, DateTime.Now.ToString("yyyy/MM/dd"), users[index].UserId);
                }
                _dbConnection.Execute(sb.ToString());
            }
        }

        public void CreateRandomCommentForQuestion()
        {
            var users = _dbConnection.Query<User>(@"select * from [User]").ToList();
            var questions = _dbConnection.Query<Question>(@"select * from Question").ToList();

            for (int i = 0; i < 10000; i++)
            {
                StringBuilder sb = new StringBuilder();
                var rndQuestion = new Random();
                var QuestionIndex = rndQuestion.Next(0, questions.Count - 1);
                var question = questions[QuestionIndex];

                var rndComment = new Random();
                var countComment = rndComment.Next(1, 10);

                for (int j = 1; j <= countComment; j++)
                {
                    var rndUser = new Random();
                    var index = rndUser.Next(0, users.Count);

                    var CommentContent = string.Format("{0} Comment {1} for Question {2}", users[index].FullName, j, question.QuestionId);

                    sb.AppendFormat(@"insert into QuestionComment(CommentContent, QuestionId, CreatedDate, UserId)
                                                                    values('{0}', {1}, '{2}', {3})",
                                    CommentContent, question.QuestionId, DateTime.Now.ToString("yyyy/MM/dd"), users[index].UserId);
                }
                _dbConnection.Execute(sb.ToString());
            }
        }
    }
}
