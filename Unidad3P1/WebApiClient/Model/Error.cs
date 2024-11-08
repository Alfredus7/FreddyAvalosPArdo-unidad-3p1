using System.Net;
using System.Runtime.Serialization;

namespace ICG.Corporate.Website.ApiClient.Model
{
    [DataContract]
    public class Error
    {
        [DataMember]
        public bool HasError;
        [DataMember]
        public int Code;
        [DataMember]
        public string Name;
        [DataMember]
        public string Message;

    }
}
