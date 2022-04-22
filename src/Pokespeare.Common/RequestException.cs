using System.Runtime.Serialization;
using System;
using System.Net;
namespace Pokespeare.Common
{
    [Serializable]
    public class RequestException : System.Exception
    {
        HttpStatusCode code;
        public RequestException(HttpStatusCode code,string message) : base(message, null) {   this.code = code;}
        protected RequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { 
               
            }

        public HttpStatusCode Code { get => code;  }
    }
}