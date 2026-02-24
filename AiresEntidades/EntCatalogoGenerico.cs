using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntCatalogoGenerico : EntAbstracta
    {
        public int IdSecundario { get; set; }
        public int ClaveId { get; set; }
        public string Clave { get; set; }
        public string Detalle { get; set; }
        public string Usuario { get; set; }
    }
}
