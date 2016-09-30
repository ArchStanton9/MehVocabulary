using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator;

namespace MehDictionary.Model
{
    public class Notebook
    {
        public List<Note> Notes { get; private set; }

        public Notebook()
        {
            Notes = new List<Note>();
        }

        public Notebook(string filepath)
        {
            Notes = Serialization.LoadTranslations(filepath);
        }
        

        public void Add(string text)
        {
            Notes.Add(new Note(text));
        }

        public void Remove(int itemID)
        {
            Notes.RemoveAll(c => c.ID == itemID);
        }

        internal void Sort()
        {
            Notes = Notes.OrderBy(s => s.Word).ToList();
        }
    }
}
