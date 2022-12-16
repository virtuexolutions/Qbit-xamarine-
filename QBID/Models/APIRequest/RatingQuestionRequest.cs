using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
   public class RatingQuestionRequest
    {
        

        public class Root
        {
            public bool success { get; set; }
            public string message { get; set; }
            public int code { get; set; }
            public List<Datum> data { get; set; }
        }
        public class Datum
        {
            public int id { get; set; }
            public string ratingQuestions { get; set; }
        }
    }
}
