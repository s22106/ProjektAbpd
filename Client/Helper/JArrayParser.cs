using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Client.Helper
{
    public static class JArrayParser
    {
        public static List<T> Parse<T>(string json) where T : class
        {
            var jarr = JArray.Parse(json);
            return jarr.ToObject<List<T>>();
        }
    }
}