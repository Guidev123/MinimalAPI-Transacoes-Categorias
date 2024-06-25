using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyFinance.Shared.Responses
{
    public class Response<TData>
    {
        private int _code = Config.DefaultStatusCode;

        [JsonConstructor]
        public Response() => _code = Config.DefaultStatusCode;


        public Response(TData? data,
                        int code = Config.DefaultStatusCode,
                        string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }


        public TData? Data { get; set; }
        public string? Message { get; set; }
        [JsonIgnore]
        public bool IsSucces => _code is >= 200 and <= 299 ;
    }
}
