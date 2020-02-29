using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class AnswerForAQuestion
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public List<AnswerComment> AnswerComments { get; set; }
        public bool IsVotedForAnswer { get; set; }
        public int TotalVote { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserFullName { get; set; }
        public string Avatar { get; set; }
        public string AvatarUrl { get; set; }
    }
}
