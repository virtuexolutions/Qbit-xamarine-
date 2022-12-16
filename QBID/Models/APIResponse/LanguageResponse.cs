using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
    public class DatumLanguage
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
    }

    public class LanguageResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public List<DatumLanguage> Data { get; set; }
    }


}
