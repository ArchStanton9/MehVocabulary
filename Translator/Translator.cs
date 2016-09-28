using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Translator
{
    public static class Translator
    {
        const string Url = @"https://translate.yandex.net/api/v1.5/tr.json/translate";
        const string API = @"trnsl.1.1.20160927T120711Z.21c65837ecdf2d78.e211814496163733422a95adaa7b1bad3732e0d0";

        struct JsonAnswer
        {
            public int Code { get; set; }
            public string Lang { get; set; }
            public List<string> Text { get; set; }
        }

        public static List<string> Translate(string text, string lang)
        {
            var requestResult = TranslationWebRequest(text, lang);
            var answer = JsonConvert.DeserializeObject<JsonAnswer>(requestResult);
            return answer.Text;
        }

        public static List<string> Translate(string text)
        {
            string lang = "en-ru";
            return Translate(text, lang);
        }

        private static string TranslationWebRequest(string text, string lang)
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
