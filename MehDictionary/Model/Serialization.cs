using System.IO; 
using Newtonsoft.Json;

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
