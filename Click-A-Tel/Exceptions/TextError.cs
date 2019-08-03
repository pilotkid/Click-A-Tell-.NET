using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ClickATel.Exceptions
{
    [Serializable]
    internal class TextMessageInvalidException : Exception
    {
        public int? errorCode { get; set; }
        public string error { get; set; }
        public string errorDescription { get; set; }
    }
}
