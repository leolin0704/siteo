using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models
{
    public class ResponseResult
    {

        public ResponseResult()
        {

        }

        public ResponseResult(int status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public ResponseResult(int status, string message, object data)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
        }

        public int Status {
            get;
            set;
        }

        public string Message {
            get;
            set;
        }

        public object Data {
            get;
            set;
        }
    }
}