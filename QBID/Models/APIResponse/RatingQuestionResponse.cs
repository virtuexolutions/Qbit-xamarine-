using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{

    public class QuestionListResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int code { get; set; }
        
        public List<Question> Data { get; set; }
    }
    public class Question
    {
        public int Id { get; set; }
        public string RatingQuestions { get; set; }
    }

}
