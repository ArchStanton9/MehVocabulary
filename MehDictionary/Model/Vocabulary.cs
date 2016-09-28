using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator;

namespace MehDictionary.Model
{
    public class Vocabulary
    {
        public List<Translation> Translations { get; private set; }

        public Vocabulary()
        {
            Translations = new List<Translation>();
        }

        public Vocabulary(string filepath)
        {
            Translations = Serialization.LoadTranslations(filepath);
        }
        
        public Vocabulary(List<Translation> translations)
        {
            Translations = translations;
        }
        public void Add(string text)
        {
                Translations.Add(new Translation(text));
        }

        public void Remove(string text)
        {
            var index = Translations.FindLastIndex(c => c.Word.ToLower() == text);
            Translations.RemoveAt(index);
        }
    }
}
