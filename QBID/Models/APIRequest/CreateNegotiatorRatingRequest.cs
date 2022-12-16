using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIRequest
{
    public class CreateNegotiatorRatingRequest
    {
        public string negotiatorId { get; set; }
        public List<QuestionAnswer> questionsAnswers { get; set; }
        public string rating { get; set; }
    }
    public class QuestionAnswer {
        public string questionId { get; set; }
        public string answer { get; set; }
    }
}
