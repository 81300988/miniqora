using Quora.BLL.Interface;
using Quora.DAL.Interface;
using Quora.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quora.BLL
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public List<Answer> AddAnswer(AddAnswerRequest model)
        {
            return _answerRepository.AddAnswer(model).ToList();
        }

        public int UpdateVoteForAnswer(UpdateVoteForAnswer model)
        {
            return _answerRepository.UpdateVoteForAnswer(model);
        }
    }
}
