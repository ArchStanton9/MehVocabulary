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

        public int Count { get { return Translations.Count;} }

        public Vocabulary()
        {
            Translations = new List<Translation>();
        }
        public void Add(string text)
        {
            Translations.Add( new Translation(text));
        }

        public void Remove(string text)
        {
            Translations.RemoveAll(s => s.Text == text);
        }
    }
}
