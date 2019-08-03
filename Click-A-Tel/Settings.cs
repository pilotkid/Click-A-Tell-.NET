using System;
using System.Collections.Generic;
using System.Text;

namespace ClickATel
{
    public static class Settings
    {
        private static string DFN = "";

        private static byte Country_Code = 1;

        internal static char[] CountryCode_Chars { get; private set; } = {'1'};

        internal static string CountryCodeStr { get; private set; } = "1";
        
        /// <summary>
        /// This is the country code that you plan on texting
        /// E.G. +1 for USA
        /// </summary>
        public static byte CountryCode
        {
            get { return Country_Code; }

            set
            {
                Country_Code = value;
                CountryCodeStr = value.ToString();
                CountryCode_Chars = CountryCodeStr.ToCharArray();
            }
        }//END METHOD

        /// <summary>
        /// This is the default from number that is required to send a text message
        /// This can also be overwriten via the message class
        /// </summary>
        public static string DefaultFromNumber
        {
            get { return DFN; }

            set { DFN = value.PhoneNumberFormatter(); }
        }
    }//END CLASS
}//END NAMESPACE
