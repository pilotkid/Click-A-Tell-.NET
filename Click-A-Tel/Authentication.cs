using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClickATel
{
    public static class Authenticate
    {
        /// <summary>
        /// CLICK A TELL API KEY
        /// </summary>
        public static string ApiKey { get; set; } = null;
        
        /// <summary>
        /// Tests to see if the API key provided is valid
        /// </summary>
        /// <param name="ThrowException">Should the function throw an exception on error or return bool</param>
        /// <returns>API key valid or not</returns>
        public static bool Test_Login(bool ThrowException=false)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://platform.clickatell.com/public-client/balance");
            requestMessage.Headers.TryAddWithoutValidation("Authorization", $"{ApiKey}");
            requestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = httpClient.SendAsync(requestMessage).Result;

            if (ThrowException)
                throw new Exception("Authentication Failed!\n\n" + response.ReasonPhrase + "\n\n" + response.Content);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }//END TEST METHOD

        /// <summary>
        /// Verifies that an API key has been provided
        /// </summary>
        internal static void VerifyAuthentication()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new Exception("You must declare an API key before you can call this action\nAdd\nApiKey = \"<YOUR API>\" \nto your code before you call this method");
            }
        }
    }//END CLASS
}//END NAMESPACE

