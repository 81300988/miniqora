using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.DAL.Interface
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> AddAnswer(AddAnswerRequest model);
        int UpdateVoteForAnswer(UpdateVoteForAnswer model);
    }
}
