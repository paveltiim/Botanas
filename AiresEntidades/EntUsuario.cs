using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntUsuario:EntCatalogoGenerico
    {
        public int CompañiaId { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int TipoUsuarioId { get; set; }
        public bool Administrador { get; set; }
        public bool Supervisor { get; set; }
        public int AlmacenMayoristaId { get; set; }
        public int EstablecimientoCajaId { get; set; }
        public int EstablecimientoClientesId { get; set; }
    }
}
