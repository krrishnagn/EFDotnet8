using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProj.Context
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public T Data { get; set; }
        public Response()
        {
        }
        public Response(T Data, string Message)
        {
            Success = true;
            this.Message = Message;
            this.Data = Data;
        }
        public Response(string Message)
        {
            Success = false;
            this.Message = Message;
        }
    }

}