using System;
using System.Collections.Generic;
using Translator;
using PdfSharp.Pdf;
using System.Text;
using PdfSharp.Drawing;
using System.IO;

namespace MehDictionary.Model
{
    static class PDFCreator
    {
        public static void WritePDF(List<Note> list, string filename)
        {
            const int Height = 830;

            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Font option to make it Unicode
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            // Create a font
            XFont font = new XFont("Calibri", 11, XFontStyle.Regular, options);

            int x = 0;
            // Draw the text
            for (int i = 0; i < list.Count; i++)
            {
                StringBuilder sb = new StringBuilder();

                string ts = list[i].Transcription;
                if (ts != null)
                    sb.Append("   [" + ts + "]     ");

                foreach (var item in list[i].Defenitions)
                {
                    sb.Append(item.Translations[0].Text + ", ");
                }

                if (sb.Length > 2)
                {
                    sb.Remove(sb.Length - 2, 2);
                }

                var s = string.Format("{0}   -    {1}", list[i].Word, sb.ToString());

                x += 10;
                gfx.DrawString(s, font, XBrushes.Black, new XRect(10, x, page.Width, page.Height), XStringFormats.TopLeft);
                if (x == 800)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    x = 0;
                }
            }
            // Save the document...

            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fullFileName = Path.Combine(desktopFolder, filename);
            
            document.Save(fullFileName);
        }

        
    }
}
