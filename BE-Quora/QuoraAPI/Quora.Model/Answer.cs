using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public int UserAnswerId { get; set; }
        public string UserFullName { get; set; }
        public string Avatar { get; set; }
        public string AvatarUrl { get; set; }
        public string AnswerDate { get; set; }
        public int TotalVote { get; set; }
        public List<AnswerComment> AnswerComments { get; set; }
    }
}
