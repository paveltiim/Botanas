using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public abstract class EntAbstracta
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public int EstatusId { get; set; }
        public DateTime Fecha { get; set; }

        public int EmpresaId { get; set; }
    }
}
