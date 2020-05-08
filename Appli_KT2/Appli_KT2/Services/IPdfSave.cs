using PdfSharpCore.Pdf;

namespace Appli_KT2.Services
{
    public interface IPdfSave
    {
        void Save(PdfDocument doc, string fileName);
    }
}
