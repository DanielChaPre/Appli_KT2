using Appli_KT2.Model;
using PdfSharp.Fonts;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using PdfSharpCore.Fonts;
using System.Reflection;
using Appli_KT2.Services;
using PdfSharp.Xamarin.Forms;
using PdfSharp.Xamarin.Forms.Delegates;


namespace Appli_KT2
{
    public class PDFSampleListRendererDelegate : PdfListViewRendererDelegate
    {
        public override void DrawCell(ListView listView, int section, int row, XGraphics page, XRect bounds, double scaleFactor)
        {
            XFont font = new XFont("times" ?? PdfSharpCore.Fonts.GlobalFontSettings.FontResolver.DefaultFontName, 2);
            var yourObject = (listView.ItemsSource as List<PlantelesES>).ElementAt(row);

            page.DrawString(yourObject.NombreInstitucionES, font, XBrushes.Black, bounds,
            new XStringFormat
            {
                LineAlignment = XLineAlignment.Far,
                Alignment = XStringAlignment.Near,
            });
        }

        public override void DrawFooter(ListView listView, int section, XGraphics page, XRect bounds, double scaleFactor)
        {
            base.DrawFooter(listView, section, page, bounds, scaleFactor);
        }

        public override double GetFooterHeight(ListView listView, int section)
        {
            return base.GetFooterHeight(listView, section);
        }
    }
}
