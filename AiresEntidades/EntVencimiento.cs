using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntVencimiento : EntAbstracta
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int DiasPlazo { get; set; }
    }
}
