using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
  public  class QueryAndComplaintResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public List<SuggestionListModel> data { get; set; }
    }
    public class SuggestionListModel
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }

    

}
