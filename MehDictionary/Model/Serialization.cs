using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 
using Newtonsoft.Json;
using Translator;

namespace MehDictionary.Model
{
    static class Serialization
    {
        public static List<Note> LoadTranslations(string filepath)
        {
            string json;
            var translations = new List<Note> ();

            try
            {
                json = File.ReadAllText(filepath);
                translations = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            catch (FileNotFoundException)
            {
                
            }
            catch (DirectoryNotFoundException)
            {

            }
            return translations;
        }

        public static void WriteTranslaionsToFile(List<Note> translations, string filepath)
        {
            var content = JsonConvert.SerializeObject(translations);
            File.WriteAllText(filepath, content);
        }
    }
}
