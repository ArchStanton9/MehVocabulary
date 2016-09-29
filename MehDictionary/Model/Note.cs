using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehDictionary.Model.Note
{
    class Note
    {
        public Word Word { get; private set; }
        public int ID { get; private set; }
        public string Translation { get; set; }
        public List<Defenition> Options { get; private set; }
        public DateTime TranslationDate { get; set; }
    }

    public class Word
    {
        public string Text { get; set; }
        public string Pos { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    class Defenition : Word
    {
        public List<Translation> Translations { get; set; }
        public string Transcription { get; set; }
    }

    class Translation : Word
    {
        public List<Word> Synonyms { get; set; }
        public List<string> Meaning { get; set; }
    }
}
