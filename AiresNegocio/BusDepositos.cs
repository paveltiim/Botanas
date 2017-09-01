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
    public class BusDepositos:BusAbstracta
    {
        public List<EntDeposito> ObtieneDepositosPorEmpresa(int EmpresaId)
        {
            try
            {
                List<EntDeposito> lst = new List<EntDeposito>();
                dt = new DatDepositos().ObtieneDepositos(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntDeposito m = new EntDeposito();
                    m.Id = Convert.ToInt32(r["DEP_ID"]);
                    //m.Descripcion = r["DEP_DESCRIPCION"].ToString();
                    m.Fecha = Convert.ToDateTime(r["DEP_FECHA"]);
                    m.Total = Convert.ToDecimal(r["DEP_TOTAL"]);
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntDeposito> ObtieneDepositos(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntDeposito> lst = new List<EntDeposito>();
                dt = new DatDepositos().ObtieneDepositos(FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntDeposito m = new EntDeposito();
                    m.Id = Convert.ToInt32(r["DEP_ID"]);
                    //m.Descripcion = r["DEP_DESCRIPCION"].ToString();
                    m.Fecha = Convert.ToDateTime(r["DEP_FECHA"]);
                    m.Total = Convert.ToDecimal(r["DEP_TOTAL"]);

                    m.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);
                    m.Pago= Convert.ToDecimal(r["PAG_PAGO"]);

                    if (!string.IsNullOrWhiteSpace(r["FAC_ID"].ToString()))
                    {
                        m.Factura = r["FAC_ID"].ToString();
                        m.FechaFactura = Convert.ToDateTime(r["FAC_FECHA"]);
                    }
                    else
                        m.Factura = "";

                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Agrega un Deposito.
        /// </summary>
        /// <param name="Deposito">
        /// Propiedades Necesarias: Total, Fecha.
        /// </param>
        public int AgregaDeposito(EntDeposito Deposito)
        {
            try
            {
                return new DatDepositos().agregaDeposito(Deposito.Total, Deposito.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza el Pago del Pedido.
        /// </summary>
        /// <param name="Pago">
        /// Propiedades Necesarias: Id, PagoId.
        /// </param>
        public void AgregaPagoDeposito(EntDeposito PagoDeposito)
        {
            try
            {
                new DatDepositos().agregaPagoDeposito(PagoDeposito.Id, PagoDeposito.PagoId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza el Deposito.
        /// </summary>
        /// <param name="Deposito">
        /// Propiedades Necesarias: Id, Total, Fecha.
        /// </param>
        public void ActualizaDeposito(EntDeposito Deposito)
        {
            try
            {
                new DatDepositos().actualizaDeposito(Deposito.Id, Deposito.Total, Deposito.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
