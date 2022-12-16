using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
       public class StateResponce
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public List<State> data { get; set; }
    }
    public class State
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
