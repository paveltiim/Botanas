using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XsPDF.Pdf;
using System.Drawing;

namespace AiresUtilerias
{
    public class PDFtoImage
    {
        public object ConvertPdfToImage(string PathPDF)
        {
            // Create a PDF converter instance by loading a local file 
            PdfImageConverter pdfConverter = new PdfImageConverter(PathPDF);

            // Set the dpi, the output image will be rendered in such resolution
            pdfConverter.DPI = 96;

            for (int i = 0; i < pdfConverter.PageCount; i++)
            {
                //// Convert each pdf page to jpeg image with original page size
                ////Image pageImage = pdfConverter.PageToImage(i);
                //// Convert pdf to jpg in customized image size
                //Image pageImage = pdfConverter.PageToImage(i, 500, 800);

                //// Save converted image to jpeg format
                //pageImage.Save("Page " + i + ".jpg", ImageFormat.Jpeg);
                return (object)pdfConverter.PageToImage(i, 500, 800);
            }
            return null;
        }
    }
}
