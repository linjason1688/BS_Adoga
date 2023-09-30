using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Service
{
    public class OperationResult
    {
        public bool IsSuccessful { get; set; }
        public Exception Exception { get; set; }
    }

    public static class OperationResultHelper
    {
        public static string WriteLog(this OperationResult value)
        {
            if (value.Exception != null)
            {
                return value.Exception.ToString();
            }
            else
            {
                return "沒有存檔";
            }
        }
    }
}