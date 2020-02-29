using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class GetQuestionbyCategoryResponse
    {
       public  List<QuestionsByCategory> Questions { get; set; }
        public int TotalQuestion { get; set; }
    }
}
