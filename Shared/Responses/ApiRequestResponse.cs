using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class ApiRequestResponse<T>
    {
        public bool IsSuccessStatusCode { get; }
        public T? Content { get; }

        public ApiRequestResponse(bool isSuccess, T? data)
        {
            IsSuccessStatusCode = isSuccess;
            Content = data;
        }
    }
}
