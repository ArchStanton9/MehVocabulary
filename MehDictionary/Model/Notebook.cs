using System.Collections.Generic;
using System.Linq;
using System.Text;
using Translator;

namespace MehVocabulary.Model
{
    public class Notebook
    {
        public List<Note> Notes { get; private set; }

        public Notebook()
        {
            Notes = new List<Note>();
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

        public string GetFullInfo(string word)
        {
            var id = Notes.Find(s => s.Word == word).ID;
            return GetFullInfo(id);
        }

        public string GetFullInfo(int id)
        {
            var info = new StringBuilder();
            var examples = new StringBuilder("\nПримеры использования: ");
            var note = Notes.Find(c => c.ID == id);

            foreach (var def in note.Defenitions)
            {
                info.Append(string.Format("\n \u23FA {0}({1}): \n", def.Text, def.Pos)); 

                foreach (var word in def.Translations)
                {
                    string text = word.Text;
                    string pos = word.Pos;

                    info.AppendFormat("\t\u23F5{0}", word.Text);

                    if (word.Meaning != null)
                    {
                        info.Append(" \u21D2");
                        foreach (var mean in word.Meaning)
                        {
                            info.AppendFormat(" {0},", mean);
                        }
                        info.Remove(info.Length - 1, 1);
                        info.Append(". \n");  
                    }
                    else
                    {
                        info.Append("\n");
                    }

                    if (word.Synonyms != null)
                    {
                        info.Append("\t\tСинонимы:");
                        foreach (var syn in word.Synonyms.Take(4))
                        {
                            info.AppendFormat(" {0},", syn);
                        }
                        info.Remove(info.Length - 1, 1);
                        info.Append(". \n");
                    }

                    if (word.Examples != null)
                    { 
                        foreach (var ex in word.Examples)
                        {
                            examples.AppendFormat("{0}", ex.Text);
                            if (ex.Translations[0] != null)
                                examples.AppendFormat(" - {0}. ", ex.Translations[0].Text);
                            else
                                examples.Append(". ");
                        }
                    }
                }
            }

            if (info.Length < 500 && examples.Length > 25)
                info.Append(examples);

            return info.ToString();
        }
    }
}
