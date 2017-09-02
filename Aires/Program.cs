using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Inicio());
        }

        public static AiresEntidades.EntEmpresa EmpresaSeleccionada { get; set; }
        public static AiresEntidades.EntUsuario UsuarioSeleccionado { get; set; }
        public static bool CambiaEmpresa { get; set; }
    }
}
