using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntUsuario:EntCatalogoGenerico
    {
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}
