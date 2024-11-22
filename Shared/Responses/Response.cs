using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class Response<T>
    {
        public Response(int httpStatusCode, T data, int code)
        {
            HttpStatusCode = httpStatusCode;
            Data = data;
            Code = code;

        }
        public Response(int httpStatusCode, T data)
        {
            HttpStatusCode = httpStatusCode;
            Data = data;
        }

        public int Code { get; set; }
        public T Data { get; set; }
        public int HttpStatusCode { get; set; }
    }
}
