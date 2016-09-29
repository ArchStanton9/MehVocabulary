using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Translator
{
    public static class Translator
    {
        const string Url = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup";
        const string API = @"dict.1.1.20160929T082251Z.dc28350833a39062.9f1ce8b2c4096da83566ded93889d85da995b675";
        const string lang = "en-ru";

        static void Main(string[] args)
        {
            var test = new Note("gun");
        }

        public static List<Defenition> Translate(string text)
        {
            var requestResult = DictionaryWebRequest(text, lang);
            Response result = JsonConvert.DeserializeObject<Response>(requestResult);
            return result.Content;
        }
        struct Response
        {
            [JsonProperty("def")]
            public List<Defenition> Content { get; set; }
        }


        public static string DictionaryWebRequest(string text, string lang)
        {
            WebRequest req = WebRequest.Create(Url + "?key=" + API + "&text=" + text + "&lang=" + lang);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }

    }
}
