using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using ClickATel.Models;
using ClickATel.Models.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ClickATel
{
    public static class MessagesActions
    {
        /// <summary>
        /// Prevent TextMessageInvalidException from being thrown
        /// NOTE: This means you will have to check if your messages were sent through the Results property
        /// </summary>
        public static bool DisableExceptions = false;

        /// <summary>
        /// Send a message to a single recipient Immediately 
        /// </summary>
        /// <param name="Msg_Content">String to send as a text message</param>
        /// <param name="SendTo">Phone number to send to</param>
        /// <returns>Models.Results.SendMessagesStatus</returns>
        public static SendMessageStatus SendMessage(string Msg_Content, string SendTo)
        {
            //CREATE NEW MESSAGE
            Message msg = new Message()
            {
                To = new List<string> {SendTo},
                Message_Text = Msg_Content
            };

            //Send Message
            SendMessagesStatus Messages = SendMessage(msg);

            //Get Message Status
            SendMessageStatus MsgStatus = Messages.Message[0];

            //Check if message was accepted
            if (!MsgStatus.Accepted)
            {
                //If exceptions are enabled throw exception with error details
                if (!DisableExceptions)
                {
                    throw new Exceptions.TextMessageInvalidException()
                    {
                        error = MsgStatus.error,
                        errorCode = MsgStatus.errorCode,
                        errorDescription = MsgStatus.errorDescription
                    };
                }
            }

            return MsgStatus;
        }//END METHOD

        /// <summary>
        /// This sends a text message to a recipient at a specified time
        /// </summary>
        /// <param name="Msg_Content">String of message to be sent</param>
        /// <param name="DeliverAt">Time at which to deliver the message</param>
        /// <param name="SendTo">Phone number to send the message to</param>
        /// <returns>Models.Results.SendMessagesStatus</returns>
        public static SendMessageStatus SendMessage(string Msg_Content, DateTime DeliverAt, string SendTo)
        {
            //CREATE NEW MESSAGE
            Message msg = new Message()
            {
                To = new List<string> { SendTo },
                Message_Text = Msg_Content,
                DeliverAt = DeliverAt
            };

            //Send Message
            SendMessagesStatus Messages = SendMessage(msg);

            //Get Message Status
            SendMessageStatus MsgStatus = Messages.Message[0];

            //Check if message was accepted
            if (!MsgStatus.Accepted)
            {
                //If exceptions are enabled throw exception with error details
                if (!DisableExceptions)
                {
                    throw new Exceptions.TextMessageInvalidException()
                    {
                        error = MsgStatus.error,
                        errorCode = MsgStatus.errorCode,
                        errorDescription = MsgStatus.errorDescription
                    };
                }
            }

            return MsgStatus;
        }//END METHOD

        /// <summary>
        /// This sends a message to up to 200 recipients at one time, immediately 
        /// </summary>
        /// <param name="Msg_Content">String of message to be sent</param>
        /// <param name="SendTo">List of recipients to send the message to</param>
        /// <returns>Models.Results.SendMessagesStatus</returns>
        public static SendMessagesStatus SendMessage(string Msg_Content, params string[] SendTo)
        {
            //CREATE NEW MESSAGE
            Message msg = new Message()
            {
                To = new List<string>(SendTo),
                Message_Text = Msg_Content
            };

            //Send Message
            SendMessagesStatus Messages = SendMessage(msg);
            return Messages;
        }//END METHOD

        /// <summary>
        /// This sends a message to up to 200 recipients at once. At a time that is specified
        /// </summary>
        /// <param name="Msg_Content">String of message to be sent</param>
        /// /// <param name="DeliverAt">Time at which to deliver the message</param>
        /// <param name="SendTo">List of recipients to send the message to</param>
        /// <returns>Models.Results.SendMessagesStatus</returns>
        public static SendMessagesStatus SendMessage(string Msg_Content, DateTime DeliverAt, params string[] SendTo)
        {
            //CREATE NEW MESSAGE
            Message msg = new Message()
            {
                To = new List<string>(SendTo),
                DeliverAt = DeliverAt,
                Message_Text = Msg_Content
            };

            //Send Message
            SendMessagesStatus Messages = SendMessage(msg);
            return Messages;
        }//END METHOD

        /// <summary>
        /// This sends a text from the Message class
        /// This is the raw function that will return a value regardless of if there is an error or not
        /// </summary>
        /// <param name="msg">Sends a message to all the numbers provided</param>
        /// <returns>Models.Results.SendMessagesStatus</returns>
        public static SendMessagesStatus SendMessage(Message msg)
        {
            //Verify Token
            Authenticate.VerifyAuthentication();

            string Req_JSON = JsonConvert.SerializeObject(msg,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "yyyy-MM-dd'T'HH:mm:sszzz",
            });

            //Make Request
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://platform.clickatell.com/messages");
            requestMessage.Headers.TryAddWithoutValidation("Authorization", $"{Authenticate.ApiKey}");
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Content = new StringContent(Req_JSON, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.SendAsync(requestMessage).Result;

            //SEE IF RESPONCE IS GOOD IF SO COLLECT JSON
            string JSON = "";
            if (response.IsSuccessStatusCode)
                JSON = response.Content.ReadAsStringAsync().Result;
            else
                throw new Exception($"Request Failed\n{response.StatusCode}-{response.ReasonPhrase}");

            //Parse Request
            SendMessagesStatus Msg = JsonConvert.DeserializeObject<SendMessagesStatus>(JSON);

            return Msg;
        }//END METHOD

        /// <summary>
        /// Gets the message status from the message that has been sent
        /// </summary>
        /// <param name="MsgStatus">Send status from the SendMessage function</param>
        /// <returns>string of the message status E.G. DELIVERED_TO_GATEWAY</returns>
        public static string GetMessageStatus(SendMessageStatus MsgStatus)
        {
            Authenticate.VerifyAuthentication();

            //Make Request
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://platform.clickatell.com/public-client/message/status?messageId={MsgStatus.MessageID}");
            requestMessage.Headers.TryAddWithoutValidation("Authorization", $"{Authenticate.ApiKey}");
            requestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = httpClient.SendAsync(requestMessage).Result;

            //SEE IF RESPONCE IS GOOD IF SO COLLECT JSON
            string JSON = "";
            if (response.IsSuccessStatusCode)
                JSON = response.Content.ReadAsStringAsync().Result;
            else
                throw new Exception($"Request Failed\n{response.StatusCode}-{response.ReasonPhrase}");

            JObject x = Tools.GetJsonValue(JSON);
            return x["status"].ToString();
        }

        /// <summary>
        /// Gets the message status from the message that has been sent
        /// </summary>
        /// <param name="MessageID">Message ID string</param>
        /// <returns>string of the message status E.G. DELIVERED_TO_GATEWAY</returns>
        public static string GetMessageStatus(string MessageID)
        {
            Authenticate.VerifyAuthentication();

            //Make Request
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://platform.clickatell.com/public-client/message/status?messageId={MessageID}");
            requestMessage.Headers.TryAddWithoutValidation("Authorization", $"{Authenticate.ApiKey}");
            requestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = httpClient.SendAsync(requestMessage).Result;

            //SEE IF RESPONCE IS GOOD IF SO COLLECT JSON
            string JSON = "";
            if (response.IsSuccessStatusCode)
                JSON = response.Content.ReadAsStringAsync().Result;
            else
                throw new Exception($"Request Failed\n{response.StatusCode}-{response.ReasonPhrase}");

            JObject x = Tools.GetJsonValue(JSON);
            return x["status"].ToString();
        }
    }//END CLASS
}//END NAMESPACE