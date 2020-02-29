using Quora.BLL.Interface;
using Quora.DAL.Interface;
using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public IEnumerable<Question> GetQuestions(int currentUserId, int pageSize, int pageIndex, out int totalQuestion)
        {
            return _questionRepository.GetQuestion(currentUserId, pageSize, pageIndex,out totalQuestion);
        }

        public List<Question> GetQuestionsByCategory(int categoryId)
        {
            return _questionRepository.GetQuestionsByCategory(categoryId);
        }

        public int AddQuestion(AddQuestionRequest model)
        {
            return _questionRepository.AddQuestion(model);
        }
        public QuestionDetail GetQuestionDetail(int questionId)
        {
            return _questionRepository.GetQuestionDetail(questionId);
        }
    }
}
