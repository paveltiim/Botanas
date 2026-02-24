using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
//using Outlook = Microsoft.Office.Interop.Outlook;
namespace AiresUtilerias
{
    public class UtiCorreo : UtiAbstracta
    {
        /*
         * Cliente SMTP
         * Gmail:  smtp.gmail.com  puerto:587
         * Hotmail: smtp.live.com  puerto:25
         */
        SmtpClient server = new SmtpClient("smtp-mail.outlook.com", 587);
        //SmtpClient server = new SmtpClient("smtp.gmail.com", 587);

        public UtiCorreo()
        {
            /*
             * Autenticacion en el Servidor
             * Utilizaremos nuestra cuenta de correo
             *
             * Direccion de Correo (Gmail o Hotmail)
             * y Contrasena correspondiente
             */

            //server.Credentials = new System.Net.NetworkCredential("tiimfacturacion@hotmail.com", "tiim10gl");
            //server.Credentials = new System.Net.NetworkCredential("tiimtecnologia@gmail.com", "tiim10gl");

            server.Credentials = new System.Net.NetworkCredential("tiimfacturacion.botanasjavy@hotmail.com", "Botanasjavy4");
            //server.Credentials = new OAuth2Credentials();
            server.EnableSsl = true;
        }

        public void MandarCorreo(MailMessage mensaje)
        {
            server.Send(mensaje);
        }

        public void EnviaCorreo(string Asunto, string Para, string Mensaje)
        {
            MailMessage mnsj = new MailMessage();

            mnsj.Subject = Asunto;

            mnsj.To.Add(new MailAddress(Para));

            mnsj.From = new MailAddress("tiimfacturacion.botanasjavy@hotmail.com", "TIIM Facturación");

            /* Si deseamos Adjuntar algún archivo*/
            //mnsj.Attachments.Add(new Attachment("C:\\archivo.pdf"));

            mnsj.Body = "  " + Mensaje + " \n\n Saludos";
            /* Enviar */
            MandarCorreo(mnsj);
        }

        public void EnviaCorreo(string Asunto, List<string> Para, string Mensaje, List<string> PathArchivosAdjuntos)
        {
            using (MailMessage mnsj = new MailMessage())
            {
                mnsj.Subject = Asunto;
                //Para[0] = "pavel_pink@hotmail.com";
                foreach (string p in Para)
                {
                    if (!string.IsNullOrWhiteSpace(p))
                        mnsj.To.Add(new MailAddress(p));
                }

                mnsj.From = new MailAddress("tiimfacturacion.botanasjavy@hotmail.com", "TIIM Facturación");

                /* Si deseamos Adjuntar algún archivo*/
                foreach (string s in PathArchivosAdjuntos)
                {
                    mnsj.Attachments.Add(new Attachment(s));
                }

                mnsj.Body = "  " + Mensaje + " \n\n Saludos";

                /* Enviar */
                MandarCorreo(mnsj);
            }
        }

        string Usuario = "pavel_tiim@hotmail.com";
        string Contraseña = "tiimFac10!";//GENERAL//"Fuckoo04!";//TIIM

        public void EnviaCorreoPADE(List<string> Para, string Contrato, string UUID)
        {
            mx.pade.timbradoCorreo.Timbrado40 wPade = new mx.pade.timbradoCorreo.Timbrado40();
            string correos = "";
            foreach (string S in Para)
            {
                if (S != null)
                    if (S.Contains("@") && S.ToUpper().Contains(".COM"))
                        correos += S + ",";
            }
            if (correos.Length == 0)
                throw new Exception("NO SE ENCONTRARON CORREOS");
            correos = correos.Remove(correos.Length - 1);
            //correos = "pavel_pink@hotmail.com";
            //Contrato = "ca2be778-3ba5-48f3-8fae-00b831faebea";
            //Usuario = "pavel_tiim@hotmail.com";
            //Contraseña = "Fuckoo06!";
            if (Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            wPade.enviarXmlAndPdfPorCorreo(Contrato, Usuario, Contraseña, UUID, correos);

        }
    }
}
