using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL.Interface
{
    public interface ISeedDataService
    {
        void CreateRandomQuestions();
        void CreateRandomAnswer();
        void CreateRandomVote();
        void CreateRandomCommentForAnswer();
        void CreateRandomCommentForQuestion();
    }
}
