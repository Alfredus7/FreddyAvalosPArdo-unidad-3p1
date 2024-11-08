using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace ICG.Corporate.Website.ApiClients.Model
{
    public class CoorporateApiError
    {
        public CoorporateApiError()
        {
            
        }

        public string Message { get; set; }

        public Dictionary<string, List<string>> ModelState { get; set; }
    }
}