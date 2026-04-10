using System;

namespace AiresEntidades
{
    public class EntReporteEntradasAjustes
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public decimal CantidadEntrada { get; set; }
        public decimal CostoEntrada { get; set; }
        public decimal TotalCostoEntrada { get; set; }

        public decimal CantidadSalidaAjuste { get; set; }
        public decimal CostoSalidaAjuste { get; set; }
        public decimal TotalCostoSalidaAjuste { get; set; }

        public decimal CantidadEntradaReal { get { return CantidadEntrada - CantidadSalidaAjuste; } }
        public decimal TotalCostoEntradaReal { get { return TotalCostoEntrada - TotalCostoSalidaAjuste; } }
    }
}
