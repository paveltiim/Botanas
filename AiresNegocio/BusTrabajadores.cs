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
    public class BusTrabajadores : BusAbstracta
    {
        public List<EntTrabajador> ObtieneTrabajadoresPorEmpresa(int EmpresaId)
        {
            try
            {
                List<EntTrabajador> lst = new List<EntTrabajador>();
                dt = new DatTrabajadores().obtieneTrabajadoresPorEmpresa(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntTrabajador c = new EntTrabajador();
                    c.Id = Convert.ToInt32(r["TRA_ID"]);
                    c.EstablecimientoId = Convert.ToInt32(r["TRA_ESTABLECIMIENTOID"]);
                    c.TipoTrabajadorId = Convert.ToInt32(r["TRA_TIPOTRABAJADORID"]);
                    c.Nombre = r["TRA_NOMBRE"].ToString();
                    c.Direccion = r["TRA_DIRECCION"].ToString();
                    c.Telefono = r["TRA_TELEFONO"].ToString();
                    c.Telefono2 = r["TRA_TELEFONO2"].ToString();
                    c.Celular = r["TRA_CELULAR"].ToString();
                    //c.Curp = r["TRA_CURP"].ToString();
                    //c.NoCredencial = r["TRA_CREDENCIAL"].ToString();
                    //c.RutaId = Convert.ToInt32(r["TRA_RUTAID"]);

                    //c.TrabajadorUID = r["TRA_UID"].ToString();
                    c.Estatus = Convert.ToBoolean(r["TRA_ESTATUS"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntTrabajador> ObtieneTrabajadoresPorEmpresa(int EmpresaId, int TipoTrabajadorId)
        {
            try
            {
                List<EntTrabajador> lst = new List<EntTrabajador>();
                dt = new DatTrabajadores().obtieneTrabajadoresPorEmpresa(EmpresaId, TipoTrabajadorId);
                foreach (DataRow r in dt.Rows)
                {
                    EntTrabajador c = new EntTrabajador();
                    c.Id = Convert.ToInt32(r["TRA_ID"]);
                    c.EstablecimientoId = Convert.ToInt32(r["TRA_ESTABLECIMIENTOID"]);
                    c.TipoTrabajadorId = Convert.ToInt32(r["TRA_TIPOTRABAJADORID"]);
                    c.Nombre = r["TRA_NOMBRE"].ToString();
                    c.Direccion = r["TRA_DIRECCION"].ToString();
                    c.Telefono = r["TRA_TELEFONO"].ToString();
                    c.Telefono2 = r["TRA_TELEFONO2"].ToString();
                    c.Celular = r["TRA_CELULAR"].ToString();
                    //c.Curp = r["TRA_CURP"].ToString();
                    //c.NoCredencial = r["TRA_CREDENCIAL"].ToString();
                    //c.RutaId = Convert.ToInt32(r["TRA_RUTAID"]);

                    //c.TrabajadorUID = r["TRA_UID"].ToString();
                    c.Estatus = Convert.ToBoolean(r["TRA_ESTATUS"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntTrabajador> ObtieneTrabajadores(int EstablecimientoId)
        {
            try
            {
                List<EntTrabajador> lst = new List<EntTrabajador>();
                dt = new DatTrabajadores().obtieneTrabajadores(EstablecimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntTrabajador c = new EntTrabajador();
                    c.Id = Convert.ToInt32(r["TRA_ID"]);
                    c.EstablecimientoId = Convert.ToInt32(r["TRA_ESTABLECIMIENTOID"]);
                    c.TipoTrabajadorId = Convert.ToInt32(r["TRA_TIPOTRABAJADORID"]);
                    c.Nombre = r["TRA_NOMBRE"].ToString();
                    c.Direccion = r["TRA_DIRECCION"].ToString();
                    c.Telefono = r["TRA_TELEFONO"].ToString();
                    c.Telefono2 = r["TRA_TELEFONO2"].ToString();
                    c.Celular = r["TRA_CELULAR"].ToString();
                    //c.Curp = r["TRA_CURP"].ToString();
                    //c.NoCredencial = r["TRA_CREDENCIAL"].ToString();
                    //c.RutaId = Convert.ToInt32(r["TRA_RUTAID"]);

                    //c.TrabajadorUID = r["TRA_UID"].ToString();
                    c.Estatus = Convert.ToBoolean(r["TRA_ESTATUS"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntTrabajador> ObtieneTrabajadores(int RutaId, int TipoTrabajadorId)
        {
            try
            {
                List<EntTrabajador> lst = new List<EntTrabajador>();
                dt = new DatTrabajadores().obtieneTrabajadores(RutaId, TipoTrabajadorId);
                foreach (DataRow r in dt.Rows)
                {
                    EntTrabajador c = new EntTrabajador();
                    c.Id = Convert.ToInt32(r["TRA_ID"]);
                    c.TipoTrabajadorId = Convert.ToInt32(r["TRA_TIPOTRABAJADORID"]);
                    c.Nombre = r["TRA_NOMBRE"].ToString();
                    c.Direccion = r["TRA_DIRECCION"].ToString();
                    c.Telefono = r["TRA_TELEFONO"].ToString();
                    c.Telefono2 = r["TRA_TELEFONO2"].ToString();
                    c.Celular = r["TRA_CELULAR"].ToString();
                    //c.Curp = r["TRA_CURP"].ToString();
                    //c.NoCredencial = r["TRA_CREDENCIAL"].ToString();
                    //c.RutaId = Convert.ToInt32(r["TRA_RUTAID"]);
                    c.Estatus = Convert.ToBoolean(r["TRA_ESTATUS"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public EntTrabajador ObtieneTrabajador(int TrabajadorId)
        {
            try
            {
                EntTrabajador c = new EntTrabajador();
                dt = new DatTrabajadores().obtieneTrabajador(TrabajadorId);
                foreach (DataRow r in dt.Rows)
                {
                    c.Id = Convert.ToInt32(r["TRA_ID"]);
                    c.EstablecimientoId = Convert.ToInt32(r["TRA_ESTABLECIMIENTOID"]);
                    c.TipoTrabajadorId = Convert.ToInt32(r["TRA_TIPOTRABAJADORID"]);
                    c.Nombre = r["TRA_NOMBRE"].ToString();
                    c.Direccion = r["TRA_DIRECCION"].ToString();
                    c.Telefono = r["TRA_TELEFONO"].ToString();
                    c.Telefono2 = r["TRA_TELEFONO2"].ToString();
                    c.Celular = r["TRA_CELULAR"].ToString();
                    //c.Curp = r["TRA_CURP"].ToString();
                    //c.NoCredencial = r["TRA_CREDENCIAL"].ToString();
                    //c.RutaId = Convert.ToInt32(r["TRA_RUTAID"]);
                    c.Estatus = Convert.ToBoolean(r["TRA_ESTATUS"]);
                }
                return c;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public EntTrabajador ObtieneCobradorPorRuta(int RutaId)
        {
            try
            {
                EntTrabajador c = new EntTrabajador();
                dt = new DatTrabajadores().obtieneCobradorPorRuta(RutaId);
                foreach (DataRow r in dt.Rows)
                {
                    c.Id = Convert.ToInt32(r["TRA_ID"]);
                    c.TipoTrabajadorId = Convert.ToInt32(r["TRA_TIPOTRABAJADORID"]);
                    c.Nombre = r["TRA_NOMBRE"].ToString();
                    c.Direccion = r["TRA_DIRECCION"].ToString();
                    c.Telefono = r["TRA_TELEFONO"].ToString();
                    c.Telefono2 = r["TRA_TELEFONO2"].ToString();
                    c.Celular = r["TRA_CELULAR"].ToString();
                    //c.Curp = r["TRA_CURP"].ToString();
                    //c.NoCredencial = r["TRA_CREDENCIAL"].ToString();
                    //c.RutaId = Convert.ToInt32(r["TRA_RUTAID"]);
                    c.Estatus = Convert.ToBoolean(r["TRA_ESTATUS"]);
                }
                return c;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Agrega nuevo Trabajador.
        /// </summary>
        /// <param name="Trabajador">
        /// Propiedades Necesarias:Nombre, NombreFiscal, Direccion, Calle, NoExterior, NoInterior, Colonia, Localidad, Municipio, Estado, CP, Telefono, Telefono2, Celular, RFC, Email, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia, Fecha.
        /// </param>
        public int AgregaTrabajador(int EmpresaId, int EstablecimientoId,EntTrabajador Trabajador)
        {
            try
            {
                return new DatTrabajadores().agregaTrabajador(EmpresaId, EstablecimientoId, Trabajador.TipoTrabajadorId, Trabajador.Nombre,
                    Trabajador.Direccion, Trabajador.Calle, Trabajador.NoExterior, Trabajador.NoInterior, Trabajador.Colonia, Trabajador.CP,
                    Trabajador.Telefono, Trabajador.Telefono2, Trabajador.Celular, Trabajador.RFC, Trabajador.Curp, Trabajador.NoCredencial,
                    Trabajador.Email, Trabajador.Email2, Trabajador.Email3,
                    Trabajador.RutaId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza nuevo Trabajador.
        /// </summary>
        /// <param name="Trabajador">
        /// Propiedades Necesarias:Id, Nombre, NombreFiscal, Direccion, Calle, NoExterior, NoInterior, Colonia, Localidad, Municipio, Estado, CP, Telefono, Telefono2, Celular, RFC, Email, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia, Fecha.
        /// </param>
        public void ActualizaTrabajador(EntTrabajador Trabajador)
        {
            try
            {
                new DatTrabajadores().actualizaTrabajador(Trabajador.Id, Trabajador.TipoTrabajadorId, Trabajador.Nombre,
                    Trabajador.Direccion, Trabajador.Calle, Trabajador.NoExterior, Trabajador.NoInterior, Trabajador.Colonia, Trabajador.CP,
                    Trabajador.Telefono, Trabajador.Telefono2, Trabajador.Celular, Trabajador.RFC, Trabajador.Curp, Trabajador.NoCredencial,
                    Trabajador.Email, Trabajador.Email2, Trabajador.Email3,
                    Trabajador.RutaId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza el Estatus del Trabajador solicitado.
        /// </summary>
        /// <param name="Trabajador">
        /// Propiedades Necesarias: Id, Estatus.
        /// </param>
        public void ActualizaEstatusTrabajador(EntTrabajador Trabajador)
        {
            try
            {
                new DatTrabajadores().actualizaEstatusTrabajador(Trabajador.Id, Trabajador.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
