using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string GetFullInfo(int id)
        {
            var info = new StringBuilder();

            var note = Notes.Find(c => c.ID == id);

            info.Append("Варианты перевода ");

            foreach (var def in note.Defenitions)
            {
                info.Append(string.Format("\n \u23FA {0}({1}): \n", def.Text, def.Pos)); 

                foreach (var word in def.Translations)
                {
                    string text = word.Text;
                    string pos = word.Pos;

                    info.AppendFormat("\t\u23F5{0} \n ", word.Text);

                    if (word.Meaning != null)
                    {
                        info.Append("\t\tЗначения:");
                        foreach (var mean in word.Meaning)
                        {
                            info.AppendFormat(" {0},", mean);
                        }
                        info.Remove(info.Length - 1, 1);
                        info.Append(". \n");  
                    }


                    if (word.Synonyms != null)
                    {
                        info.Append("\t\tСинонимы:");
                        foreach (var syn in word.Synonyms)
                        {
                            info.AppendFormat(" {0},", syn);
                        }
                        info.Remove(info.Length - 1, 1);
                        info.Append(". \n");
                    }

                    if (word.Examples != null)
                    {
                        info.Append("\t\tПримеры использования:\n");
                        foreach (var ex in word.Examples)
                        {
                            info.AppendFormat("\t\t\t {0}", ex.Text);
                            if (ex.Translations[0] != null)
                                info.AppendFormat(" - {0}.\n", ex.Translations[0].Text);
                            else
                                info.Append(".\n");
                        }
                        info.Remove(info.Length - 1, 1);
                        info.Append("\n");
                    }
                }
            }

            return info.ToString();
        }
    }
}
