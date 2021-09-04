using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace AiresUtilerias
{
    public class UtiCorreo : UtiAbstracta
    {
        /*
         * Cliente SMTP
         * Gmail:  smtp.gmail.com  puerto:587
         * Hotmail: smtp.live.com  puerto:25
         */
        SmtpClient server = new SmtpClient("smtp.live.com", 587);
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

            server.Credentials = new System.Net.NetworkCredential("tiimfacturacion@hotmail.com", "tiim10gl");
            //server.Credentials = new System.Net.NetworkCredential("tiimtecnologia@gmail.com", "tiim10gl");
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

            mnsj.From = new MailAddress("tiimfacturacion@hotmail.com", "Serdan Refrigeración");

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

                mnsj.From = new MailAddress("tiimfacturacion@hotmail.com", "TIIM Facturación");

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

    }
}
