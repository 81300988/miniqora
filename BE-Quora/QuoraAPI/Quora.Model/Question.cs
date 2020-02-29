using System;

namespace Quora.Model
{
    [Serializable]
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CategoryName { get; set; }
        public string ImageName { get; set; }
        public string CategoryImageUrl { get; set; }
        public int AnswerId { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime TimeOfAnswer { get; set; }
        public string AnswerContent { get; set; }
        public bool IsVotedForAnswer { get; set; }
        public int TotalVote { get; set; }
        public int TotalComment { get; set; }
    }
}
