using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace MehDictionary.Helpers
{
    public static class PDFHelper
    {
        const int fontSize = 11;
        const string fontFamilyName = "Calibri";
        const int height = 800;
        const int step = 10;

        static int carriage;
        static XPdfFontOptions options;
        static XFont font;
        static XGraphics gfx;
        static PdfPage page;

        static PDFHelper()
        {
            options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            font = new XFont(fontFamilyName, fontSize, XFontStyle.Regular, options);
        }

        public static void AddLine(this PdfDocument document, string text)
        {
            if (page == null)
                page = document.AddPage();

            if (carriage == height)
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                carriage = 0;
            }

            gfx.DrawString(text, font, XBrushes.Black, new XRect(10, carriage, page.Width, page.Height), XStringFormats.TopLeft);

            carriage += step;
        }
    }
}
