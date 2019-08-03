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

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; } = Settings.DefaultFromNumber;

        [JsonProperty(PropertyName = "scheduledDeliveryTime")]
        public DateTime? DeliverAt { get; set; } = null;

        [JsonProperty(PropertyName = "content")]
        public string Message_Text { get; set; }
    }
}
