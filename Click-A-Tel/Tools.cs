using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClickATel
{
    static class Tools
    {
        public static JObject GetJsonValue(string JSON)
        {
            return JObject.Parse(JSON);
        }
    }
}
