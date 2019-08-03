using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ClickATel.Models.Results
{
    public class SendMessagesStatus
    {
        [JsonProperty(PropertyName = "messages")]
        public List<SendMessageStatus> Message { get; set; }
    }

    public class SendMessageStatus
    {
        [JsonProperty(PropertyName = "apiMessageId")]
        public string MessageID { get; set; }

        [JsonProperty(PropertyName = "accepted")]
        public bool Accepted { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string toNumber { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public int? errorCode { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string error { get; set; }

        [JsonProperty(PropertyName = "errorDescription")]
        public string errorDescription { get; set; }
    }
}
