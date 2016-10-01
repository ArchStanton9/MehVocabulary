using System;
using System.Collections.Generic;
using Translator;
using PdfSharp.Pdf;
using System.Text;
using MehDictionary.Helpers;
using System.IO;
using System.Linq;

namespace MehDictionary.Model
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

                    sb.AppendFormat("{0}     -     ", note.Word);

                    if (note.Transcription != null)
                        sb.Append(" [ " + note.Transcription + " ] ");

                    var translations = note.Defenitions
                        .Select(d => d.Translations[0]);

                    foreach (var tr in translations)
                    {
                        sb.Append(tr.Text + ", ");
                    }

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
