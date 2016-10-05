using System;
using System.Collections.Generic;
using Translator;
using PdfSharp.Pdf;
using System.Text;
using MehVocabulary.Helpers;
using System.IO;
using System.Linq;

namespace MehVocabulary.Model
{
    static class PDFCreator
    {
        public static void WritePDF(List<Note> list, string filename)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            var dictionary = list
                .GroupBy(w => w.Word[0].ToString().ToUpper())
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var group in dictionary)
            {
                document.PrintHeader(group.Key.ToString().ToUpper());

                foreach (var note in group.Value)
                {
                    var sb = new StringBuilder();

                    var text = note.Word;
                    text = text.Substring(0, 1).ToUpper() + text.Remove(0, 1);

                    sb.AppendFormat("{0}     -     ", text);

                    if (note.Transcription != null)
                        sb.Append(" [ " + note.Transcription + " ] ");

                    var translations = note.Defenitions
                        .SelectMany(d => d.Translations);

                    foreach (var tr in translations)
                    {
                        if (sb.Length < 80)
                            sb.Append(tr.Text + ", ");
                    }

                    if (sb.Length > 2)
                        sb.Remove(sb.Length - 2, 2);
                    sb.Append(".");

                    document.AddLine(sb.ToString());
                }

                document.AddLine(" ");
            }


            // Save the document...

            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fullFileName = Path.Combine(desktopFolder, filename);
            
            document.Save(fullFileName);
        }
    }
}
