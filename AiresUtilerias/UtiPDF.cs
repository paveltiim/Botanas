using Ghostscript.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresUtilerias
{
    public class UtiPDF:UtiAbstracta
    {
        public void LoadImage(string InputPDFFile, int PageNumber, string OutputPath)
        {

            string outImageName = Path.GetFileNameWithoutExtension(InputPDFFile);
            outImageName = outImageName + "_" + PageNumber.ToString() + "_.png";


            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png256);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(290, 290);
            dev.InputFiles.Add(InputPDFFile);
            dev.Pdf.FirstPage = PageNumber;
            dev.Pdf.LastPage = PageNumber;
            dev.CustomSwitches.Add("-dDOINTERPOLATE");
            dev.OutputPath = OutputPath;//+"/"+outImageName;// Server.MapPath(@"~/tempImages/" + outImageName);
            dev.Process();

        }
        void LeeEditaPDF()
        {
            //// open the reader
            //PdfReader reader = new PdfReader(pathSourcePDF);
            //iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);
            //Document document = new Document(size);

            //// open the writer
            //FileStream fs = new FileStream(pathOutputPDF, FileMode.Create, FileAccess.Write);
            //PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //document.Open();

            //// the pdf content
            //PdfContentByte cb = writer.DirectContent;

            //// select the font properties
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //cb.SetColorFill(BaseColor.DARK_GRAY);
            //cb.SetFontAndSize(bf, 8);

            //// write the text in the pdf content
            //cb.BeginText();
            //string text = "Some random blablablabla...";
            //// put the alignment and coordinates here
            //cb.ShowTextAligned(0, text, 400, 300, 0);
            //cb.EndText();
            ////cb.BeginText();
            ////text = "Other random blabla...";
            ////// put the alignment and coordinates here
            ////cb.ShowTextAligned(2, text, 100, 200, 0);
            ////cb.EndText();

            //// create the new page and add it to the pdf
            //PdfImportedPage page = writer.GetImportedPage(reader, 1);
            //cb.AddTemplate(page, 0, 0);

            //// close the streams and voilá the file should be changed :)
            //document.Close();
            //fs.Close();
            //writer.Close();
            //reader.Close();

            //using (var reader2 = new PdfReader(pathSourcePDF))
            //{
            //    using (var fileStream = new FileStream(pathOutputPDF, FileMode.Create, FileAccess.Write))
            //    {
            //        var document2 = new Document(reader2.GetPageSizeWithRotation(1));
            //        var writer2 = PdfWriter.GetInstance(document2, fileStream);

            //        document2.Open();

            //        for (var i = 1; i <= reader2.NumberOfPages; i++)
            //        {
            //            document2.NewPage();

            //            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //            var importedPage = writer2.GetImportedPage(reader2, i);

            //            var contentByte = writer2.DirectContent;
            //            contentByte.BeginText();
            //            contentByte.SetFontAndSize(baseFont, 12);

            //            var multiLineString = "Hello,\r\nWorld!".Split('\n');

            //            foreach (var line in multiLineString)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, line, 100, 400, 0);
            //            }

            //            contentByte.EndText();
            //            contentByte.AddTemplate(importedPage, 0, 0);
            //        }

            //        document2.Close();
            //        writer2.Close();
            //    }
            //}


            //using (var reader2 = new PdfReader(pathSourcePDF))
            //{
            //    using (var fileStream = new FileStream(pathOutputPDF, FileMode.Create, FileAccess.Write))
            //    {
            //        var document2 = new Document(reader2.GetPageSizeWithRotation(1));
            //        var writer2 = PdfWriter.GetInstance(document2, fileStream);

            //        document2.Open();

            //        for (var i = 1; i <= reader2.NumberOfPages; i++)
            //        {
            //            document2.NewPage();

            //            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //            var importedPage = writer2.GetImportedPage(reader2, i);

            //            var contentByte = writer2.DirectContent;
            //            contentByte.BeginText();
            //            contentByte.SetFontAndSize(baseFont, 12);

            //            //var multiLineString = "Hello, World!".Split('\n');
            //            //***BUSCACOORDENADAS***
            //            //string textSearch = "TOTAL";
            //            var parser = new PdfReaderContentParser(reader2);

            //            var strategy = parser.ProcessContent(1, new LocationTextExtractionStrategyWithPosition());

            //            var res = strategy.GetLocations();

            //            var searchResult = res.Where(p => p.Text.Contains("TOTAL")).OrderBy(p => p.Y).Reverse().ToList();
            //            //****//


            //            //setting up the X and Y coordinates of the document
            //            int x = 62;
            //            int y = Convert.ToInt32(searchResult[1].Y) + 118;

            //            y = (int)(792 - y);

            //            //foreach (var line in multiLineString)
            //            //{
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Hello, World!", x, y, 0);
            //            //}

            //            contentByte.EndText();
            //            contentByte.AddTemplate(importedPage, 0, 0);
            //        }

            //        document2.Close();
            //        writer2.Close();
            //    }
            //}

        }
        ////Our test file
        //var testFile = iTextSharp.text.pdf.parser.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");

        ////Create our test file, nothing special
        //using (var fs = new FileStream(testFile, FileMode.Create, FileAccess.Write, FileShare.None))
        //{
        //    using (var doc = new Document())
        //    {
        //        using (var writer = PdfWriter.GetInstance(doc, fs))
        //        {
        //            doc.Open();

        //            doc.Add(new Paragraph("This is my sample file"));

        //            doc.Close();
        //        }
        //    }
        //}

        ////Create an instance of our strategy
        //var t = new MyLocationTextExtractionStrategy();

        ////Parse page 1 of the document above
        //using (var r = new PdfReader(testFile))
        //{
        //    var ex = PdfTextExtractor.GetTextFromPage(r, 1, t);
        //}

        ////Loop through each chunk found
        //foreach (var p in t.myPoints)
        //{
        //    Console.WriteLine(string.Format("Found text {0} at {1}x{2}", p.Text, p.Rect.Left, p.Rect.Bottom));
        //}

        //// Creamos el documento con el tamaño de página tradicional
        //Document doc = new Document(PageSize.LETTER);
        //// Indicamos donde vamos a guardar el documento
        //PdfWriter writer = PdfWriter.GetInstance(doc,
        //                            new FileStream(@"C:\prueba.pdf", FileMode.Create));

        //// Le colocamos el título y el autor
        //// **Nota: Esto no será visible en el documento
        //doc.AddTitle("Mi primer PDF");
        //doc.AddCreator("Roberto Torres");

        //// Abrimos el archivo
        //doc.Open();
        //string path = "C:/TIIM/Facturacion/Facturas/RAFAEL GIL ARMENTA/20170105121745/64DD0F9F-A637-4BEA-A03B-D3DEEF45D8FE.pdf";
        //string pathDestination = "C:/TIIM/Facturacion/Facturas/RAFAEL GIL ARMENTA/20170105121745/prueba.png";

        //UtiPDF pdfConverter = new UtiPDF();
        //pdfConverter.LoadImage(path, 1, pathDestination);

        //PdfReader reader = new PdfReader(path);
        //int intPageNum = reader.NumberOfPages;
        //string[] words;
        //string line;

        //for (int i = 1; i <= intPageNum; i++)
        //{
        //    string text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());

        //    words = text.Split('\n');
        //    for (int j = 0, len = words.Length; j < len; j++)
        //    {
        //        line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j]));
        //    }
        //}

        //PDFParser pdf = new PDFParser();
        ////pdf.ExtractText(path, pathDestination);

        //// Creamos el documento PDF
        //iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.LETTER);
        //iTextSharp.text.Document doc2 = new iTextSharp.text.Document(PageSize.LETTER);
        ////PdfWriter writer2 = PdfWriter.GetInstance(doc2,
        ////                new FileStream(@"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105121745\64DD0F9F-A637-4BEA-A03B-D3DEEF45D8FE.pdf", FileMode.Append));
        //PdfWriter writer = PdfWriter.GetInstance(doc,
        //                new FileStream(@"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105121745\prueba.pdf", FileMode.Create));
        //doc.Open();
        ////PdfTemplate template = new PdfTemplate();


        //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:\Users\pavel\OneDrive\TIIM\tImagenes\IMAGENES AIRES\mirage.png");
        //imagen.BorderWidth = 0;
        //imagen.Alignment = Element.ALIGN_LEFT;
        //float percentage = 0.0f;
        //percentage = 150 / imagen.Width;
        //imagen.ScalePercent(50);

        //// Insertamos la imagen en el documento
        //doc.Add(imagen);

        ////imagen = iTextSharp.text.Image.GetInstance(@"C:\Users\pavel\OneDrive\TIIM\tImagenes\IMAGENES AIRES\mirage.png");
        ////imagen.BorderWidth = 0;
        ////imagen.Alignment = Element.ALIGN_RIGHT;
        ////percentage = 0.0f;
        ////percentage = 150 / imagen.Width;
        ////imagen.ScalePercent(50);

        //////// Insertamos la imagen en el documento
        ////doc.Add(imagen);
        //// Creamos la imagen y le ajustamos el tamaño
        //byte[] buffer;
        //using (Stream stream = new FileStream(@"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105121745\64DD0F9F-A637-4BEA-A03B-D3DEEF45D8FE.pdf", FileMode.Open))
        //{
        //    buffer = new byte[stream.Length - 1];
        //    stream.Read(buffer, 0, buffer.Length);

        //    //iTextSharp.text.Image imagen1 = iTextSharp.text.Image.GetInstance(buffer);
        //    //imagen1.BorderWidth = 0;
        //    //imagen1.Alignment = Element.ALIGN_LEFT;

        //    // Insertamos la imagen en el documento
        //    string texto = pdf.ExtractTextFromPDFBytes(buffer);
        //};

        ////doc.NewPage();
        ////PDFParser pdf = new PDFParser();

        //////PdfTextExtract.pdfText(path);
        ////PdfReader reader = new PdfReader(path);
        ////string texto = "";
        ////pdf.ExtractText(path, pathDestination);

        ////iTextSharp.text.Image imagen1 = iTextSharp.text.Image.GetInstance(reader.GetPageContent(1));
        ////imagen1.BorderWidth = 0;
        ////imagen1.Alignment = Element.ALIGN_LEFT;

        ////reader.AddPdfObject();
        //// Insertamos la imagen en el documento
        ////doc.Add(reader.AddPdfObject(imagen));

        ////doc.Add(new Paragraph(texto));
        ////doc.Add(reader.GetPageContent(1));

        ////writer.AddDirectImageSimple(imagen);

        //// Cerramos el documento
        //doc.Close();
        //writer.Close();
    }
}
