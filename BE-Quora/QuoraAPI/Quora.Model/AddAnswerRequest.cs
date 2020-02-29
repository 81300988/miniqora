using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class AddAnswerRequest
    {
        public string AnswerContent { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string CreatedDate
        {
            get
            {
                return DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }
        }
    }
}
