using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class SelectServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public List<ServiceData> Data { get; set; }

    }
    public class ServiceData
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
    }

}
