using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClickATel
{
    internal static class Tools
    {
        public static JObject GetJsonValue(string JSON)
        {
            return JObject.Parse(JSON);
        }

        public static string PhoneNumberFormatter(this string value)
        {
            value = new Regex(@"\D").Replace(value, string.Empty);
            value = value.TrimStart('+');
            value = value.TrimStart(Settings.CountryCode_Chars);

            if (value.Length == 0)
                throw new Exception("Phone number is empty!");

            if (value.Length < 3)
                value = string.Format("({0})", value.Substring(0, value.Length));
            else if (value.Length < 7)
                value = string.Format("({0}){1}", value.Substring(0, 3), value.Substring(3, value.Length - 3));
            else if (value.Length < 11)
                value = string.Format("({0}){1}-{2}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6));
            else if (value.Length > 10)
            {
                value = value.Remove(value.Length - 1, 1);
                value = string.Format("({0}){1}-{2}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6));
            }

            Console.WriteLine("STRING FORMATTER");

            return string.Format("+{0}{1}", Settings.CountryCodeStr, value);
        }//END METHOD
    }//END CLASS
}//END NAMESPACE
