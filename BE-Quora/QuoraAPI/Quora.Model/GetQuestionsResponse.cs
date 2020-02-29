using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class GetQuestionsResponse
    {
        public List<Question> Questions { get; set; }
        public int TotalQuestion { get; set; }
    }
}
