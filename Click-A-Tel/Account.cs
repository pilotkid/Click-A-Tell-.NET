using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClickATel
{
    /// <summary>
    /// Allows you to perform actions on the account
    /// </summary>
    public static class Account
    {
        /// <summary>
        /// Gets the account balance
        /// </summary>
        /// <returns>Account Balance</returns>
        public static decimal GetBalance()
        {
            return GetBalance(out string ignore);
        }//END METHOD


        /// <summary>
        /// Gets the account balance and currency type
        /// </summary>
        /// <param name="Currency">Out string for the currency type</param>
        /// <returns>Decimal of account balance</returns>
        public static decimal GetBalance(out string Currency)
        {
            //Get Balance Tuple
            GetBalance(out Tuple<decimal, string> Vals);

            //Set the currency string
            Currency = Vals.Item2;

            //Return the decimal value
            return Vals.Item1;
        }//END METHOD

        /// <summary>
        /// Gets the account balance and currency type
        /// output via tuple
        /// </summary>
        /// <param name="Value">Out tuple of the balance and currency type</param>
        public static void GetBalance(out Tuple<decimal, string> Value)
        {
            //Verify Token
            Authenticate.VerifyAuthentication();

            //Make Request
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://platform.clickatell.com/public-client/balance");
            requestMessage.Headers.TryAddWithoutValidation("Authorization", $"{Authenticate.ApiKey}");
            requestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = httpClient.SendAsync(requestMessage).Result;

            //SEE IF RESPONCE IS GOOD IF SO COLLECT JSON
            string JSON = "";
            if (response.IsSuccessStatusCode)
                JSON = response.Content.ReadAsStringAsync().Result;
            else
                throw new Exception($"Request Failed\n{response.StatusCode}-{response.ReasonPhrase}");

            //Parse Request
            JObject x = Tools.GetJsonValue(JSON);
            decimal bal = decimal.Parse(x["balance"].ToString());
            string currency = x["currency"].ToString();


            //Set Tuple and return
            Value = new Tuple<decimal, string>(bal,currency);
        }//END METHOD
    }//END CLASS
}//END NAMESPACE
