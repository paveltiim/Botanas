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
    }
}
