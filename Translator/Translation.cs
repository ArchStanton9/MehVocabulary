using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public class Translation
    {
        public List<string> Options { get; set; }
        public DateTime TranslationDate { get; set; }
        private string word;
        public string Word {
            get
            {
                return word;
            }
            set
            {
                word = value;
                Options = Translate();
            }
        }
        public Translation(string text)
        {
            Word = text;
            Options = Translate();
        }

        private List<string> Translate()
        {
            TranslationDate = DateTime.Now;
            return Translator.Translate(word);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var item in Options)
            {
                builder.Append(item + " ");
            }
            return builder.ToString();
        }
    }
}