﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models
{
    public enum ResponseResultStatus {
        SUCCESS = 1,
        FAILED = 2,
        NO_PERMISSION = 3,
        NOT_LOGIN = 4,
        VALIDATION_FAILED = 5
    }

    public class ResponseResult
    {

        public ResponseResult()
        {

        }

        public ResponseResult(ResponseResultStatus status, string message)
        {
            this.Status = (int)status;
            this.Message = message;
        }

        public ResponseResult(ResponseResultStatus status, List<string> messageList)
        {
            this.Status = (int)status;
            this.MessageList = messageList;
        }

        public ResponseResult(ResponseResultStatus status, string message, object data)
        {
            this.Status = (int)status;
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

        public List<string> MessageList
        {
            get;
            set;
        }


        public object Data {
            get;
            set;
        }
    }
}