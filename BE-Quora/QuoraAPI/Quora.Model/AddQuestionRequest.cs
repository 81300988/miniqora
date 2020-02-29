using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class AddQuestionRequest
    {
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }
        public List<int> CategoriesId { get; set; }
        public string CreatedDate
        {
            get
            {
                return DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }
        }
        public int UserId { get; set; }
    }
}
