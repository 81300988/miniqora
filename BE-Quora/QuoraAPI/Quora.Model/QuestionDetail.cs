using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class QuestionDetail
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }
        public string QuestionPotster { get; set; }
        public List<AnswerForAQuestion> Answers { get; set; }
        public List<QuestionComment> Comments { get; set; }


    }
}
