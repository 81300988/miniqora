using Quora.Model;
using System.Collections.Generic;

namespace Quora.DAL.Interface
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetQuestion(int currentUserId, int pageSize, int pageIndex, out int totalQuestion);
        List<Question> GetQuestionsByCategory(int catogoryId);
        int AddQuestion(AddQuestionRequest model);
        QuestionDetail GetQuestionDetail(int questionId);
    }
}
