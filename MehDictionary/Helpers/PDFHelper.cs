using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace MehVocabulary.Helpers
{
    public static class PDFHelper
    {
        const int fontSize = 11;
        const string fontFamilyName = "Calibri";
        const int height = 800;
        const int step = 11;

        static int carriage;
        static XPdfFontOptions options;
        static XFont font;
        static XFont headerFont;
        static XGraphics gfx;
        static PdfPage page;

        static PDFHelper()
        {
            options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            font = new XFont(fontFamilyName, fontSize, XFontStyle.Regular, options);
            headerFont = new XFont(fontFamilyName, fontSize + 2, XFontStyle.Bold, options);
        }

        public static void AddLine(this PdfDocument document, string text)
        {
            if (page == null || height - carriage <  10)
                NextPage(document);

            gfx.DrawString(text, font, XBrushes.Black, new XRect(10, 10 + carriage, page.Width, page.Height), XStringFormats.TopLeft);

            carriage += step;
        }

        public static void PrintHeader(this PdfDocument document, string text)
        {
            if (page == null || height - carriage < 50)
                NextPage(document);

            gfx.DrawString(text, headerFont, XBrushes.Black, new XRect(10, 10 + carriage, page.Width, page.Height), XStringFormats.TopLeft);

            carriage += step;
        }

        private static void NextPage(PdfDocument document)
        {
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            carriage = 0;
        }
    }
}
