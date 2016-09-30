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
        public static Notebook LoadNotebook(string filepath)
        {
            string json;
            var notebook = new Notebook();

            try
            {
                json = File.ReadAllText(filepath);
                notebook = JsonConvert.DeserializeObject<Notebook>(json);
            }
            catch (FileNotFoundException)
            {
                
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory("Data");
            }
            return notebook;
        }

        public static void WriteTranslaionsToFile(Notebook notebook, string filepath)
        {
            var content = JsonConvert.SerializeObject(notebook, Formatting.Indented);
            File.WriteAllText(filepath, content);
        }
    }
}
