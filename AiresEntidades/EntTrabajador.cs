using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntTrabajador : EntCliente
    {
        public int TipoTrabajadorId { get; set; }
        public string TipoTrabajador { get; set; }
        public string Curp { get; set; }
        public string NoCredencial { get; set; }
        public int RutaId { get; set; }
        public string TrabajadorUID { get; set; }
    }
}
