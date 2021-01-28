using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MacroTrackApi.Utils
{
    public class JsonUtils
    {
        public static Secrets LoadSecrets()
        {
            using var r = new StreamReader("./secrets.json");
            var json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Secrets>(json);
        }

        public class Secrets
        {
            public string Database { get; set; }
            public string TwilioAccountSid { get; set; }
            public string TwilioAuthToken { get; set; }

        }
    }
}