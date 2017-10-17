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
    public class BusVencimiento : BusAbstracta
    {
        public List<EntVencimiento> ObtieneVencimientoActual()
        {
            try
            {
                List<EntVencimiento> lst = new List<EntVencimiento>();
                dt = new DatVencimiento().obtieneVencimientoActivo();
                foreach (DataRow r in dt.Rows)
                {
                    EntVencimiento c = new EntVencimiento();
                    c.Id = Convert.ToInt32(r["VEN_ID"]);
                    c.FechaInicio = Convert.ToDateTime(r["VEN_FECHAINICIO"]);
                    c.FechaVencimiento = Convert.ToDateTime(r["VEN_FECHAVENCIMIENTO"]);
                    c.Estatus = Convert.ToBoolean(r["VEN_ESTATUS"]);
                    c.DiasPlazo = Convert.ToInt32(r["VEN_DIASPLAZO"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void VerificaVencimiento()
        {
            try
            {
                new DatVencimiento().verificaVencimiento();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntCatalogoGenerico ObtieneRegistroSincronizacion(DateTime Fecha)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatVencimiento().obtieneRegistroSincronizacion(Fecha);
                EntCatalogoGenerico c = new EntCatalogoGenerico();
                foreach (DataRow r in dt.Rows)
                {
                    c.Id = Convert.ToInt32(r["REG_ID"]);
                    c.Fecha = Convert.ToDateTime(r["REG_FECHA"]);
                    //c.Estatus = Convert.ToBoolean(r["VEN_ESTATUS"]);
                    //lst.Add(c);
                }
                return c;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public EntCatalogoGenerico ObtieneUltimoRegistroSincronizacion()
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatVencimiento().obtieneUltimoRegistroSincronizacion();
                EntCatalogoGenerico c = new EntCatalogoGenerico();
                foreach (DataRow r in dt.Rows)
                {
                    c.Id = Convert.ToInt32(r["REG_ID"]);
                    c.Fecha = Convert.ToDateTime(r["REG_FECHA"]);
                    //c.Estatus = Convert.ToBoolean(r["VEN_ESTATUS"]);
                    //lst.Add(c);
                }
                return c;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void AgregarRegistroSincronizacion(DateTime Fecha)
        {
            try
            {
                new DatVencimiento().agregarRegistroSincronizacion(Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
