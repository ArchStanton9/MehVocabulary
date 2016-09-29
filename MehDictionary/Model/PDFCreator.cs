using System;
using System.Collections.Generic;
using Translator;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace MehDictionary.Model
{
    static class PDFCreator
    {
        public static void WritePDF(List<Note> list, string filename)
        {
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

            // Draw the text
            for (int i = 0; i < list.Count; i++)
            {
                gfx.DrawString(list[i].Word + " - " + list[i].Translation, font, XBrushes.Black, new XRect(10, 10 + 10 * i, page.Width, page.Height), XStringFormats.TopLeft);
            }
            // Save the document...
            document.Save(filename);
        }

        
    }
}
