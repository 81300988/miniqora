using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL.Interface
{
    public interface IAnswerService
    {
        List<Answer> AddAnswer(AddAnswerRequest model);
        int UpdateVoteForAnswer(UpdateVoteForAnswer model);

    }
}
