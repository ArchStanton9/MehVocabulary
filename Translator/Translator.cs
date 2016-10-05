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

        const string TrUrl = @"https://translate.yandex.net/api/v1.5/tr.json/translate";
        const string TrAPI = @"trnsl.1.1.20160927T120711Z.21c65837ecdf2d78.e211814496163733422a95adaa7b1bad3732e0d0";

        const string lang = "en-ru";

        public static List<Defenition> Translate(string text)
        {
            var requestResult = DictionaryWebRequest(Url, API ,text);
            Response result = JsonConvert.DeserializeObject<Response>(requestResult);
            
            if(result.Content.Capacity == 0)
            {
                requestResult = DictionaryWebRequest(TrUrl, TrAPI, text);
                var tr = JsonConvert.DeserializeObject<Response>(requestResult);
                result.Content.Add(new Defenition() { Text = tr.Text[0], Pos="phrase", Translations = new List<Translation>() { new Translation() { Text = tr.Text[0]} } });
            }
            
            return result.Content;
        }
        struct Response
        {
            [JsonProperty("def")]
            public List<Defenition> Content { get; set; }
            [JsonProperty("text")]
            public List<string> Text { get; set; }
        }

        public static string DictionaryWebRequest(string url, string api, string text)
        {
            WebRequest req = WebRequest.Create(url + "?key=" + api + "&text=" + text + "&lang=" + lang);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }

    }
}
