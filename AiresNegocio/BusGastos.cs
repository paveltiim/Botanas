using AiresDatos;
using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresNegocio
{
    public class BusGastos:BusAbstracta
    {
        public List<EntGasto> ObtieneEmpresaGastosPorEmpresa(int TipoGastoId, int EmpresaId)
        {
            try
            {
                List<EntGasto> lst = new List<EntGasto>();
                dt = new DatGastos().ObtieneGastos(TipoGastoId,EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntGasto m = new EntGasto();
                    m.Id = Convert.ToInt32( r["GAS_ID"]);
                    m.Descripcion = r["GAS_DESCRIPCION"].ToString();
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaGasto(EntGasto Gasto)
        {
            try
            {
                return new DatGastos().agregaGasto(Gasto.EmpresaId, Gasto.TipoGastoId, Gasto.NumeroFactura, Gasto.Cantidad, Gasto.Pago, Gasto.FechaFactura, Gasto.Observaciones, Gasto.FechaRegistro);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza el Pago del Pedido.
        /// </summary>
        /// <param name="Pago">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        public void AgregaPagoGasto(EntPago Pago)
        {
            try
            {
                new DatGastos().agregaPagoGasto(Pago.GastoId, Pago.TipoPagoId, Pago.Cantidad, Pago.FechaPago);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaGasto(EntGasto Gasto)
        {
            try
            {
                new DatGastos().actualizaGasto(Gasto.Id,Gasto.EmpresaId, Gasto.NumeroFactura, Gasto.Cantidad, Gasto.FechaFactura, Gasto.Observaciones, Gasto.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void AumentaPagoGasto(EntGasto Gasto)
        {
            try
            {
                new DatGastos().aumentaPagoGasto(Gasto.Id, Gasto.Cantidad);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
