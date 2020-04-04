using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SubjectSelection.Models
{
    public class ResponseData
    {
        public bool Status { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public int TotalCount { get; set; }

        public ResponseData()
        {
            Code = string.Empty;
            Message = string.Empty;
        }
    }
}