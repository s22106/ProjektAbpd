using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektAbpd.Client.Helper
{
    public static class HttpConverter
    {
        public static async Task<string> ConvertHttpMessage(HttpResponseMessage msg)
        {
            msg.EnsureSuccessStatusCode();
            return await msg.Content.ReadAsStringAsync();
        }
    }
}