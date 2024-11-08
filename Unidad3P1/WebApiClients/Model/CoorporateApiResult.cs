using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unidad3P1.WebApiClients
{
    public class CoorporateApiResult<T>
    {
        public CoorporateApiResult()
        {
            Errors = new Dictionary<string, List<string>>();
        }
        public T Data { get; set; }

        public Dictionary<string, List<string>> Errors { get; }

        public void AddError(string error, string key = "")
        {
            if(Errors.ContainsKey(key))
            {
                Errors[key].Add(error);
            }
            else
            {
                Errors.Add(key, new List<string>() { error });
            }
            
        }

        public bool IsValid 
        { 
            get
            {
                return !Errors.Any();
            }
        }

        public string ToStringError
        {
            get
            {
                if (Errors != null && Errors.Any())
                {
                    return JsonConvert.SerializeObject(Errors);
                }
                return string.Empty;
            }
        }
    }
}