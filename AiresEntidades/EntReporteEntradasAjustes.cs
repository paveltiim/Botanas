using System;

namespace AiresEntidades
{
    public class EntReporteEntradasAjustes
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        /// <summary>
        /// Inventory movement type id (MOINV_TIPOMOVIMIENTOINVENTARIOID).
        /// 6 = Reingreso cancelación; any other value = regular entry.
        /// </summary>
        public int TipoId { get; set; }

        /// <summary>"REINGRESO CANCELACION" when TipoId==6, "ENTRADAS" otherwise.</summary>
        public string TipoNombre { get; set; }

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
