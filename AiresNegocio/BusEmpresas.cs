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
    public class BusEmpresas : BusAbstracta
    {
        public List<EntEmpresa> ObtieneEmpresas()
        {
            try
            {
                List<EntEmpresa> lst = new List<EntEmpresa>();

                dt = new DatEmpresas().obtieneEmpresas();
                foreach (DataRow r in dt.Rows)
                {
                    EntEmpresa m = new EntEmpresa();
                    m.Id = Convert.ToInt32(r["EMP_ID"]);

                    //m.TipoPersonaId = Convert.ToInt32(r["EMP_TIPOPERSONAID"]);
                    m.Descripcion = r["EMP_NOMBRE"].ToString();
                    m.Nombre = r["EMP_NOMBRE"].ToString();
                    m.NombreFiscal = r["EMP_NOMBREFISCAL"].ToString();
                    m.RegimenFiscal = r["EMP_REGIMENFISCAL"].ToString();
                    m.Direccion = r["EMP_DIRECCION"].ToString();
                    m.Telefono = r["EMP_TELEFONO"].ToString();
                    m.Telefono2 = r["EMP_TELEFONO2"].ToString();
                    m.Email = r["EMP_EMAIL"].ToString();

                    m.RFC = r["EMP_RFC"].ToString();
                    m.Calle = r["EMP_CALLE"].ToString();
                    m.NoExterior = r["EMP_NOEXTERIOR"].ToString();
                    m.NoInterior = r["EMP_NOINTERIOR"].ToString();
                    m.Colonia = r["EMP_COLONIA"].ToString();
                    m.Localidad = r["EMP_LOCALIDAD"].ToString();
                    m.Municipio = r["EMP_MUNICIPIO"].ToString();
                    m.Estado = r["EMP_ESTADO"].ToString();
                    m.CP = r["EMP_CP"].ToString();

                    m.Contacto = r["EMP_CONTACTO"].ToString();
                    m.TelefonoContacto = r["EMP_TELEFONOCONTACTO"].ToString();
                    m.Banco = r["EMP_BANCO"].ToString();
                    m.NumeroCuenta = r["EMP_NUMEROCUENTA"].ToString();
                    m.Sucursal = r["EMP_SUCURSAL"].ToString();
                    m.CLABE = r["EMP_CLABE"].ToString();
                    m.NumeroReferencia = r["EMP_NUMEROREFERENCIA"].ToString();

                    m.Certificado = r["EMP_CERTIFICADORUTA"].ToString();
                    m.Key = r["EMP_KEYRUTA"].ToString();
                    //m.Clave = r["EMP_CLAVE"].ToString();

                    m.NoCertificado = r["EMP_NOCERTIFICADO"].ToString();
                    //m.TipoTasaIVAId = Convert.ToInt32(r["EMP_TIPOTASAIVAID"]);


                    m.TipoFactorId = 1;//Convert.ToInt32(r["EMP_TIPOFACTORID"]);
                    m.TipoFactor = "Tasa";//r["CATFAC_DESCRIPCION"].ToString();
                    m.TasaOCuota = 0.16m;//Convert.ToDecimal(r["EMP_TASAOCUOTA"]);


                    //m.RegimenFiscalId = Convert.ToInt32(r["EMP_REGIMENFISCALID"]);
                    //m.RegimenFiscal = r["CATREG_DESCRIPCION"].ToString();

                    //m.UsoCFDIId = Convert.ToInt32(r["EMP_USOCFDIID"]);

                    //m.Timbres = Convert.ToInt32(r["TIM_TIMBRESCONTRATADOS"]);
                    //m.TimbresUsados = Convert.ToInt32(r["TIM_TIMBRESUSADOS"]);

                    //m.TimbresRestantes = Convert.ToInt32(r["TIMBRESRESTANTES"]);
                    //m.Timbres = Convert.ToInt32(r["TIMBRESRESTANTES"]);

                    //m.Deuda = Convert.ToDecimal(r["GAS_CANTIDAD"]);
                    //m.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    //m.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);

                    //m.Fecha = Convert.ToDateTime(r["EMP_FECHAREGISTRO"]);
                    //m.Facturacion= Convert.ToBoolean(r["EMP_FACTURACION"]);

                    m.UsuarioId = Convert.ToInt32(r["EMP_USUARIOID"]);
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Gastos con ESTATUSID=1
        /// </summary>
        /// <returns></returns>
        
        /// <summary>
        /// Agrega una Empresa.
        /// </summary>
        /// <param name="empresa">
        /// Propiedades Necesarias: TipoPersonaId, Nombre, NombreFiscal, RegimenFiscal, Direccion, Calle, NoExterior, NoInterior, Colonia, Localidad, Municipio, Estado, CP, Telefono, Telefono2, RFC, Email, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia, Certificado, Key, Clave, TipoTasaIVAId, NoCertificado.
        /// </param>
        public int AgregaEmpresa(EntEmpresa Empresa)
        {
            try
            {
                return new DatEmpresas().agregaEmpresa(Empresa.TipoPersonaId, Empresa.Nombre, Empresa.NombreFiscal, Empresa.RegimenFiscal, Empresa.Direccion, Empresa.Calle, Empresa.NoExterior, Empresa.NoInterior, Empresa.Colonia, Empresa.Localidad, Empresa.Municipio, Empresa.Estado, Empresa.CP, Empresa.Telefono, Empresa.Telefono2, Empresa.RFC, Empresa.Email, Empresa.Banco, Empresa.NumeroCuenta, Empresa.Sucursal, Empresa.CLABE, Empresa.NumeroReferencia, Empresa.Certificado, Empresa.Key, Empresa.Clave, Empresa.TipoTasaIVAId, Empresa.NoCertificado);
                //return 0;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        //public int AgregaNotaCredito(EntPago NotaCredito)
        //{
        //    try
        //    {
        //        return new DatEmpresas().agregaNotaCredito(NotaCredito.GastoId, NotaCredito.Cantidad, NotaCredito.Fecha);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        /// <summary>
        /// Actualiza nueva Empresa.
        /// </summary>
        /// <param name="empresa">
        /// Propiedades Necesarias: Id, TipoPersonaId, Nombre, NombreFiscal, RegimenFiscal, Direccion, Calle, NoExterior, NoInterior, Colonia, Localidad, Municipio, Estado, CP, Telefono, Telefono2, RFC, Email, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia, Certificado, Key, Clave, TipoTasaIVAId, NoCertificado.
        /// </param>
        public int ActualizaEmpresa(EntEmpresa Empresa)
        {
            try
            {
                return new DatEmpresas().actualizaEmpresa(Empresa.Id, Empresa.TipoPersonaId, Empresa.Nombre, Empresa.NombreFiscal, 
                    Empresa.RegimenFiscal, Empresa.Direccion, Empresa.Calle, Empresa.NoExterior, Empresa.NoInterior, Empresa.Colonia, 
                    Empresa.Localidad, Empresa.Municipio, Empresa.Estado, Empresa.CP, Empresa.Telefono, Empresa.Telefono2, 
                    Empresa.RFC, Empresa.Email, Empresa.Banco, Empresa.NumeroCuenta, Empresa.Sucursal, Empresa.CLABE, 
                    Empresa.NumeroReferencia, Empresa.Certificado, Empresa.Key, Empresa.Clave,
                         Empresa.TipoTasaIVAId, Empresa.NoCertificado, Empresa.RegimenFiscalId, Empresa.TipoFactorId, Empresa.TasaOCuota, Empresa.UsoCFDIId);

                //return 0;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza el Estatus de la Empresa solicitada.
        /// </summary>
        /// <param name="Empresa">
        /// Propiedades Necesarias: Id, Estatus.
        /// </param>
        public int ActualizaEstatusEmpresa(EntEmpresa Empresa)
        {
            try
            {
                return new DatEmpresas().actualizaEstatusEmpresa(Empresa.Id, Empresa.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AumentaTimbreEmpresa(int EmpresaId)
        {
            try
            {
                return new DatEmpresas().aumentaTimbresEmpresa(EmpresaId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public List<EntCatalogoGenerico> ObtieneProductosServicios()
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatEmpresas().obtieneProductosServicios();
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico m = new EntCatalogoGenerico();
                    m.Id = Convert.ToInt32(r["CATPRO_ID"]);
                    m.Clave = r["CATPRO_CLAVE"].ToString();
                    m.Descripcion = r["CATPRO_DESCRIPCION"].ToString();
                    //m.EmpresaId = Convert.ToInt32(r["EMPPROSER_ID"]);
                    //m.Estatus = Convert.ToBoolean(r["EMPPROSER_ESTATUS"]);
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCatalogoGenerico> ObtieneUnidades()
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatEmpresas().obtieneUnidades();
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico m = new EntCatalogoGenerico();
                    m.Id = Convert.ToInt32(r["CATUNI_ID"]);
                    m.Clave = r["CATUNI_CLAVE"].ToString();
                    m.Descripcion = r["CATUNI_DESCRIPCION"].ToString();
                    //m.EmpresaId = Convert.ToInt32(r["EMPUNI_ID"]);
                    //m.Estatus = Convert.ToBoolean(r["EMPUNI_ESTATUS"]);
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntCatalogoGenerico> ObtieneCatalogoRegimen()
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatEmpresas().obtieneCatalogoRegimen();
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico m = new EntCatalogoGenerico();
                    m.Id = Convert.ToInt32(r["CATREG_ID"]);
                    m.Descripcion = r["CATREG_ID"].ToString() + " - " + r["CATREG_DESCRIPCION"].ToString();

                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCatalogoGenerico> ObtieneCatalogoUsoCFDI()
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatEmpresas().obtieneCatalogoUsoCFDI();
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico m = new EntCatalogoGenerico();
                    m.Id = Convert.ToInt32(r["CATUSO_ID"]);
                    m.Clave = r["CATUSO_CLAVE"].ToString();
                    m.Descripcion = r["CATUSO_CLAVE"].ToString() + " - " + r["CATUSO_DESCRIPCION"].ToString();

                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntCatalogoGenerico> ObtieneEstablecimientos(int EmpresaId)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatEmpresas().obtieneEstablecimientos(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico m = new EntCatalogoGenerico();
                    m.Id = Convert.ToInt32(r["EST_ID"]);
                    m.Descripcion= r["EST_NOMBRE"].ToString();
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
