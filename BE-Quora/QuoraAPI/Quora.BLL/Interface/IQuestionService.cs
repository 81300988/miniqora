using Quora.Model;
using System.Collections.Generic;
using System.Linq;

namespace Quora.BLL.Interface
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetQuestions(int currentUserId, int pageSize, int pageIndex, out int totalQuestion);
        List<Question> GetQuestionsByCategory(int categoryId);
        int AddQuestion(AddQuestionRequest model);
        QuestionDetail GetQuestionDetail(int questionId);
    }
}
