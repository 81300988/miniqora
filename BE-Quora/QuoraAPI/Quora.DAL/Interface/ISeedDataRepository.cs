using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.DAL.Interface
{
    public interface ISeedDataRepository
    {
        void CreateRandomQuestions();
        void CreateRandomAnswer();
        void CreateRandomVote();
        void CreateRandomCommentForAnswer();
        void CreateRandomCommentForQuestion();
    }
}
