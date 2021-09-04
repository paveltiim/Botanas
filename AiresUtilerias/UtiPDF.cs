using Ghostscript.NET;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            string outImageName = System.IO.Path.GetFileNameWithoutExtension(InputPDFFile);
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


        public class LocationTextExtractionStrategyWithPosition : LocationTextExtractionStrategy
        {
            private readonly List<TextChunk> locationalResult = new List<TextChunk>();

            private readonly ITextChunkLocationStrategy tclStrat;

            public LocationTextExtractionStrategyWithPosition() : this(new TextChunkLocationStrategyDefaultImp())
            {
            }

            /**
             * Creates a new text extraction renderer, with a custom strategy for
             * creating new TextChunkLocation objects based on the input of the
             * TextRenderInfo.
             * @param strat the custom strategy
             */
            public LocationTextExtractionStrategyWithPosition(ITextChunkLocationStrategy strat)
            {
                tclStrat = strat;
            }

            private bool StartsWithSpace(string str)
            {
                if (str.Length == 0) return false;
                return str[0] == ' ';
            }

            private bool EndsWithSpace(string str)
            {
                if (str.Length == 0) return false;
                return str[str.Length - 1] == ' ';
            }

            /**
             * Filters the provided list with the provided filter
             * @param textChunks a list of all TextChunks that this strategy found during processing
             * @param filter the filter to apply.  If null, filtering will be skipped.
             * @return the filtered list
             * @since 5.3.3
             */

            private List<TextChunk> filterTextChunks(List<TextChunk> textChunks, ITextChunkFilter filter)
            {
                if (filter == null)
                {
                    return textChunks;
                }

                var filtered = new List<TextChunk>();

                foreach (var textChunk in textChunks)
                {
                    if (filter.Accept(textChunk))
                    {
                        filtered.Add(textChunk);
                    }
                }

                return filtered;
            }

            public override void RenderText(TextRenderInfo renderInfo)
            {
                LineSegment segment = renderInfo.GetBaseline();
                if (renderInfo.GetRise() != 0)
                { // remove the rise from the baseline - we do this because the text from a super/subscript render operations should probably be considered as part of the baseline of the text the super/sub is relative to 
                    Matrix riseOffsetTransform = new Matrix(0, -renderInfo.GetRise());
                    segment = segment.TransformBy(riseOffsetTransform);
                }
                TextChunk tc = new TextChunk(renderInfo.GetText(), tclStrat.CreateLocation(renderInfo, segment));
                locationalResult.Add(tc);
            }

            public IList<TextLocation> GetLocations()
            {

                var filteredTextChunks = filterTextChunks(locationalResult, null);
                filteredTextChunks.Sort();

                TextChunk lastChunk = null;

                var textLocations = new List<TextLocation>();

                foreach (var chunk in filteredTextChunks)
                {

                    if (lastChunk == null)
                    {
                        //initial
                        textLocations.Add(new TextLocation
                        {
                            Text = chunk.Text,
                            X = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[0]),
                            Y = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[1])
                        });

                    }
                    else
                    {
                        if (chunk.SameLine(lastChunk))
                        {
                            var text = "";
                            // we only insert a blank space if the trailing character of the previous string wasn't a space, and the leading character of the current string isn't a space
                            if (IsChunkAtWordBoundary(chunk, lastChunk) && !StartsWithSpace(chunk.Text) && !EndsWithSpace(lastChunk.Text))
                                text += ' ';

                            text += chunk.Text;

                            textLocations[textLocations.Count - 1].Text += text;

                        }
                        else
                        {

                            textLocations.Add(new TextLocation
                            {
                                Text = chunk.Text,
                                X = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[0]),
                                Y = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[1])
                            });
                        }
                    }
                    lastChunk = chunk;
                }

                //now find the location(s) with the given texts
                return textLocations;

            }

        }

        public class TextLocation
        {
            public float X { get; set; }
            public float Y { get; set; }

            public string Text { get; set; }
        }
        int CuentaCaracteresHastaEspacio(string Descripcion)
        {
            if (char.IsWhiteSpace(Descripcion[Descripcion.Length - 1]))
                return 1;
            else
                return 1 + CuentaCaracteresHastaEspacio(Descripcion.Remove(Descripcion.Length - 1));
        }
        /// <summary>
        /// Método recursivo que divide Descripcion en renglones dependiendo de la longitud de la Descripcion 
        /// y de la LongitudRenglon.
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <param name="Graphic"></param>
        /// <param name="Font"></param>
        /// <param name="FontHeight"></param>
        /// <param name="Pen"></param>
        /// <param name="StartX"></param>
        /// <param name="StartY"></param>
        /// <param name="Offset"></param>
        /// <param name="LongitudRenglon">Longitud límite del renglon</param>
        /// <returns></returns>
        int EscribeRenglonesDescripciones(PdfContentByte Pbover, string TextoEscribe, int x, int y, iTextSharp.text.Font Font)
        {
            int caracteresHastaEspacio = 1;
            int LongitudRenglon = 245;
            if (TextoEscribe.Length >= LongitudRenglon)
            {
                if (char.IsWhiteSpace(TextoEscribe[LongitudRenglon - 1]))
                    ColumnText.ShowTextAligned(Pbover, Element.ALIGN_LEFT, new Phrase(TextoEscribe.Remove(LongitudRenglon - 1), Font), x, y, 0);
                else
                {
                    caracteresHastaEspacio = CuentaCaracteresHastaEspacio(TextoEscribe.Remove(LongitudRenglon - caracteresHastaEspacio));
                    ColumnText.ShowTextAligned(Pbover, Element.ALIGN_LEFT, new Phrase(TextoEscribe.Remove(LongitudRenglon - caracteresHastaEspacio), Font), x, y, 0);
                }
                y = EscribeRenglonesDescripciones(Pbover, TextoEscribe.Remove(0, LongitudRenglon - caracteresHastaEspacio), x, y - 10, Font);
            }
            else
            {
                ColumnText.ShowTextAligned(Pbover, Element.ALIGN_LEFT, new Phrase(TextoEscribe, Font), x, y, 0);
                y += 13;// FontHeight;
            }
            return y;
        }
        /// <summary>
        /// Devuelve la Ruta del Nuevo Archivo Modificado.
        /// </summary>
        /// <param name="TextoEscribe"></param>
        /// <param name="RutaArchivoModifica"></param>
        /// <param name="VersionModificada"></param>
        /// <returns></returns>
        public string ModificaPDF(string TextoEscribe, string RutaArchivoModifica, string VersionModificada, int NumProductos)
        {
            string pathSourcePDF = RutaArchivoModifica;
            //FileInfo file = new FileInfo(RutaArchivoModifica);
            string pathOutputPDF = RutaArchivoModifica.Remove(RutaArchivoModifica.Length - 4) + "-" + VersionModificada + ".pdf";
            //string pathOutputPDF = RutaArchivoModifica;

            //create PdfReader object to read from the existing document
            using (PdfReader reader3 = new PdfReader(pathSourcePDF))
            {
                //create PdfStamper object to write to get the pages from reader 
                using (PdfStamper stamper = new PdfStamper(reader3, new FileStream(pathOutputPDF, FileMode.Create)))
                {
                    //select two pages from the original document
                    reader3.SelectPages("1");

                    //gettins the page size in order to substract from the iTextSharp coordinates
                    var pageSize = reader3.GetPageSize(1);

                    // PdfContentByte from stamper to add content to the pages over the original content
                    PdfContentByte pbover = stamper.GetOverContent(1);

                    //add content to the page using ColumnText
                    iTextSharp.text.Font font = new iTextSharp.text.Font();
                    font.Size = 5;

                    //***BUSCACOORDENADAS***
                    //string textSearch = "SELLO DEL SAT";
                    //string textSearch = "ESTE DOCUMENTO ES";
                    string textSearch = "CADENA ORIGINAL DEL"; 
                    //int cant = ListaProductos.Count;
                    //if (cant > 1)
                    //{
                    //    textSearch = "SUBTOTAL"; //"TOTAL";
                    //    sumaY = 180;
                    //    tamañoImagen = 70;
                    //}

                    var parser = new PdfReaderContentParser(reader3);

                    var strategy = parser.ProcessContent(1, new LocationTextExtractionStrategyWithPosition());

                    var res = strategy.GetLocations();

                    var searchResult = res.Where(p => p.Text.Contains(textSearch)).OrderBy(p => p.Y).Reverse().ToList();

                    int x = 30;
                    int y = (Convert.ToInt32(searchResult[0].Y)); //+ sumaY);///cant;

                    if(NumProductos==1)
                        y -= 10;
                    else
                        y -= 46;
                    // (Convert.ToInt32(searchResult[0].Y))*4; // (int)(pageSize.Height -800); //- cant;
                    //Creates an image that is the size i need to hide the text i'm interested in removing
                    //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ImagenMuestra, BaseColor.WHITE);
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(new Bitmap(801, 104), BaseColor.WHITE);
                    
                    //Sets the position that the image needs to be placed (ie the location of the text to be removed)
                    image.SetAbsolutePosition(x - 35, y-15);
                    //Adds the image to the output pdf
                    stamper.GetOverContent(1).AddImage(image, true);


                    iTextSharp.text.Font fontTitulo = new iTextSharp.text.Font();
                    fontTitulo.Size = 8;
                    fontTitulo.IsBold();
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER,
                                               new Phrase("ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI", fontTitulo), x + 265, y + 76, 0);

                    //setting up the X and Y coordinates of the document
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER,
                                               new Phrase("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ", font), x + 140, y + 66, 0);


                    y += 60;// (Convert.ToInt32(searchResult[0].Y))*4; // (int)(pageSize.Height -800); //- cant;
                    y = EscribeRenglonesDescripciones(pbover, TextoEscribe, x-10, y, font);
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER,
                                               new Phrase("Firma: ____________________________", font), x + 250, y - 25, 0);
                }
            }

            FileInfo file = new FileInfo(pathOutputPDF);
            file.Replace(pathSourcePDF, RutaArchivoModifica.Remove(RutaArchivoModifica.Length - 4) + "-BACKUP.pdf");
            file.Delete();
            new FileInfo(RutaArchivoModifica.Remove(RutaArchivoModifica.Length - 4) + "-BACKUP.pdf").Delete();
            return pathSourcePDF;
        }

        public void CreaPDF(List<string> RutasArchivoBmpOrigen, string RutaArchivoSalida)
        {
            Document document = new Document();
            using (var stream = new FileStream(RutaArchivoSalida, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();

                foreach (string s in RutasArchivoBmpOrigen)
                {
                    using (var imageStream = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var image = iTextSharp.text.Image.GetInstance(imageStream);
                        image.ScaleToFitHeight = true;

                        image.ScaleToFit(530, 1070);
                        document.Add(image);
                    }
                }

                document.Close();
                document.Dispose();
            }
        }
    }
}
