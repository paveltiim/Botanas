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
    public class BusDivisas:BusAbstracta
    {
        public EntDivisa ObtieneDivisa(int DivisaId)
        {
            try
            {
                EntDivisa c = new EntDivisa();
                dt = new DatDivisas().obtieneDivisa(DivisaId);
                foreach (DataRow r in dt.Rows)
                {
                    c.Id = Convert.ToInt32(r["DIV_ID"]);
                    c.Descripcion = r["DIV_DESCRIPCION"].ToString();
                    c.Valor = Convert.ToDecimal(r["DIV_VALOR"]);
                    c.Estatus = Convert.ToBoolean(r["DIV_ESTATUS"]);
                }
                return c;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
