using Quora.BLL.Interface;
using Quora.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL
{
    public class SeedDataService : ISeedDataService
    {
        private readonly ISeedDataRepository _seedDataRepository;

        public SeedDataService(ISeedDataRepository seedDataRepository)
        {
            _seedDataRepository = seedDataRepository;
        }

        public void CreateRandomAnswer()
        {
            _seedDataRepository.CreateRandomAnswer();
        }

        public void CreateRandomQuestions()
        {
            _seedDataRepository.CreateRandomQuestions();
        }

        public void CreateRandomVote()
        {
            _seedDataRepository.CreateRandomVote();
        }

        public void CreateRandomCommentForAnswer()
        {
            _seedDataRepository.CreateRandomCommentForAnswer();
        }

        public void CreateRandomCommentForQuestion()
        {
            _seedDataRepository.CreateRandomCommentForQuestion();
        }
    }
}
