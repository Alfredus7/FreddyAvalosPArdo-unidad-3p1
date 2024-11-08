using System.Net;
using System.Runtime.Serialization;

namespace Unidad3P1.WebApiClients
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
