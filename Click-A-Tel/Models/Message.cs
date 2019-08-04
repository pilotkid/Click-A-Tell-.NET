using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ClickATel.Models
{
    public class Message
    {
        private List<string> to = new List<string> { };

        [JsonProperty(PropertyName = "to")]
        public List<string> To {
            get
            {
                return to;
            }

            set
            {
                to = value;

                for (int i = 0; i < to.Count; i++)
                {
                    string to1 = to[i];
                    to[i] = to[i].PhoneNumberFormatter();
                    i++;
                }
            }
        }

        private string from_num;

        [JsonProperty(PropertyName = "from")]
        public string From
        {
            get
            {
                if (string.IsNullOrWhiteSpace(from_num))
                    return Settings.DefaultFromNumber;
                else
                    return from_num;
            }

            set => from_num = value;
        }

        [JsonProperty(PropertyName = "scheduledDeliveryTime")]
        public DateTime? DeliverAt { get; set; } = null;

        [JsonProperty(PropertyName = "content")]
        public string Message_Text { get; set; }
    }
}
