using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class QuestionsByCategory
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageName { get; set; }
        public string CategoryImageUrl { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }
        public string Categories { get; set; }
        public string Questioner { get; set; }
        public string Avatar { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime TimeOfAnswer { get; set; }
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public bool IsVotedForAnswer { get; set; }
        public int TotalVote { get; set; }
        public int TotalComment { get; set; }
    }
}
