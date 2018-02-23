using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common
{
    public class SerializeHelper
    {
        public static string toJsonString(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T toJson<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
