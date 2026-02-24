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
    public class BusFacturas : BusAbstracta
    {
        public List<EntFactura> ObtieneFacturasPorPedido(int PedidoId)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                dt = new DatFacturas().obtieneFacturasPorPedido(PedidoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);

                    p.UUID = r["FAC_UUID"].ToString();
                    //p.Facturado = Convert.ToBoolean(r["FACTURADO"]);
                    if (p.EstatusId == 1)
                        p.EstatusDescripcion = "VIGENTE";
                    else
                        p.EstatusDescripcion = "CANCELADA";

                    p.SerieFactura = r["FAC_SERIEFACTURA"].ToString();
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    p.Ruta = r["FAC_RUTA"].ToString();
                    if (!(r["FAC_PDF"] is DBNull))
                        p.PDF = (byte[])r["FAC_PDF"];
                    if (!(r["FAC_XML"] is DBNull))
                        p.XML = (byte[])r["FAC_XML"];

                    p.MetodoPagoId = Convert.ToInt32(r["FAC_METODOPAGOID"]);
                    p.FormaPagoId = Convert.ToInt32(r["FAC_FORMAPAGOID"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntFactura> ObtieneFacturasPedidosPorComplemento(string Complemento, int EmpresaId)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                dt = new DatFacturas().obtieneFacturasPedidosPorComplemento(Complemento, EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    //p.PedidoId = Convert.ToInt32(r["FAC_PEDIDOID"]);
                    p.PedidoId = Convert.ToInt32(r["FACPED_PEDIDOID"]);

                    p.SerieFactura= r["FAC_SERIEFACTURA"].ToString();
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    p.UUID = r["FAC_UUID"].ToString();

                    p.PagoId = Convert.ToInt32(r["PAG_ID"]);
                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.PagoFactura = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);

                    p.Saldo = Convert.ToDecimal(r["PED_TOTAL"]); ;
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntFactura> ObtieneFacturasPorComplemento(string Complemento, int EmpresaId)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                dt = new DatFacturas().obtieneFacturasPorComplemento(Complemento, EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    p.Id = Convert.ToInt32(r["COMPAG_FACTURAID"]);

                    p.SerieFactura = r["FAC_SERIEFACTURA"].ToString();
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    p.UUID = r["FAC_UUID"].ToString();

                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.PagoFactura = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);

                    p.Saldo = Convert.ToDecimal(r["PED_TOTAL"]); ;
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntFactura> ObtieneFacturaConPagos(int FacturaId)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                int facturaId = 0, cont = 0;
                decimal saldo = 0, pago = 0, notacredito = 0;

                dt = new DatFacturas().obtieneFacturaPedidoConPagos(FacturaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    //p.Empresa = Empresa;
                    //p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.PedidoId = Convert.ToInt32(r["PED_ID"]);
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    base.ErrorId = p.Id;
                    //if (errorId == 225)
                    //    errorId = errorId;
                    if (facturaId != p.Id)
                    {
                        facturaId = p.Id;
                        saldo = 0;
                        cont = 0;
                    }

                    //p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    //p.Cliente = r["CLI_NOMBRE"].ToString();

                    //if (p.Cliente.Contains('-'))
                    //{
                    //    p.Codigo = p.Cliente.Split('-')[0];
                    //    p.Cliente = p.Cliente.Split('-')[1];
                    //}
                    //else
                    //{
                    //    p.Codigo = r["CLI_NOMBRE"].ToString();
                    //    p.Cliente = r["CLI_NOMBREFISCAL"].ToString();
                    //}

                    //p.ClienteCodigo = r["CLI_CODIGO"].ToString();
                    //p.Cliente = r["CLI_NOMBRE"].ToString();
                    //p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();

                    p.SerieFactura = r["FAC_SERIEFACTURA"].ToString();
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    //p.Factura = p.SerieFactura + p.Factura;// Serie + p.Factura;
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);

                    p.FormaPagoId = Convert.ToInt32(r["FAC_FORMAPAGOID"]);
                    //p.FormaPago = r["CATFOR_DESCRIPCION"].ToString();

                    p.MetodoPagoId = Convert.ToInt32(r["FAC_METODOPAGOID"]);
                    //if (p.MetodoPagoId == 2)
                    //    p.MetodoPago = r["catmet_CLAVE"].ToString() + " - " + r["CATMET_DESCRIPCION"].ToString().Remove(22);
                    //else
                    //    p.MetodoPago = r["catmet_CLAVE"].ToString() + " - " + r["CATMET_DESCRIPCION"].ToString();

                    //p.Observaciones = r["CCONDICIONPAGO"].ToString();

                    p.IVA = Convert.ToDecimal(r["PED_IVA"]);
                    p.IEPS = Convert.ToDecimal(r["PED_IEPS"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);

                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTATUS"].ToString();
                    if (p.EstatusId == 1 && Convert.ToInt32(r["PED_ESTATUSID"]) == 2)
                    {
                        p.EstatusId = 2;
                        p.EstatusDescripcion += "/PAGADA";
                    }


                    //if (Convert.ToBoolean(p.EstatusId))
                    //    p.EstatusDescripcion = "VIGENTE";// r["ESTPED_DESCRIPCION"].ToString();
                    //else
                    //    p.EstatusDescripcion = "CANCELADA";

                    //p.TipoPedidoId = Convert.ToInt16(r["FAC_TIPOCOMPROBANTEID"]); //1 - FACTURA; OTRO - CP.
                    //p.TipoPedido = r["TIPOCOMPROBANTE"].ToString();

                    p.UUID = r["FAC_UUID"].ToString();
                    p.Ruta = r["FAC_RUTA"].ToString();
                    //if (!(r["FAC_PDF"] is DBNull))
                    //    p.PDF = (byte[])r["FAC_PDF"];
                    //if (!(r["FAC_XML"] is DBNull))
                    //    p.XML = (byte[])r["FAC_XML"];

                    //p.NumOrden = Convert.ToInt32(r["PED_NUMEROPEDIDOID"]).ToString();
                    p.Descripcion = r["PED_DETALLE"].ToString();
                    //p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    //p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();                    
                    //p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHAREGISTRO"]);

                    string cp = r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                    if (!string.IsNullOrWhiteSpace(cp))
                        cp = "CP-" + cp;
                    if (saldo == 0)//Primer registro Factura
                    {
                        saldo = p.Total;
                        p.Saldo = saldo;
                        p.Pago = 0;
                        p.Descripcion = "FACTURA";
                        lst.Add(p);
                        pago = Convert.ToDecimal(r["PAG_PAGO"]); //Convert.ToDecimal(r["FACING_PAGO"]);
                        notacredito = Convert.ToDecimal(r["NOTCRE_MONTO"]);

                        if (pago > 0)
                        {
                            saldo -= pago;
                            EntFactura pp = new EntFactura()
                            {
                                //Empresa = p.Empresa,
                                PagoId = 1,
                                Descripcion = "PAGO",
                                //FormaPago = r["CATFOR_DESCRIPCION"].ToString(),
                                //FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]),
                                //FechaCorta = Convert.ToDateTime(r["PAG_FECHAPAGO"]).ToShortDateString(),
                                Saldo = saldo,//p.Saldo - pago,
                                Pago = pago,
                                Id = Convert.ToInt32(r["PAG_ID"]),//p.Id
                                //NumOrdenRelacion = cp,
                                //ClienteCodigo = p.ClienteCodigo,
                                //Cliente = p.Cliente,
                                //ClienteNombreFiscal = p.ClienteNombreFiscal,
                                SerieFactura = p.SerieFactura,
                                NumeroFactura = p.NumeroFactura,
                                Fecha = p.Fecha,
                                FormaPagoId = p.FormaPagoId,
                                //FormaPago=p.FormaPago,
                                MetodoPagoId = p.MetodoPagoId,
                                //TipoMonedaId = p.TipoMonedaId,
                                //Moneda = p.Moneda,
                                //TipoCambio = p.TipoCambio,
                                IVA = 0,
                                IEPS = 0,
                                Total = 0,
                                //TotalUSD = p.Total,
                                PagoFactura = p.Total,
                                EstatusId = p.EstatusId,
                                EstatusDescripcion = "",
                                //TipoPedidoId = p.TipoPedidoId,
                                //TipoPedido = "PAGO",
                                //FechaEntrega = Convert.ToDateTime(r["FAC_FECHAREGISTRO"])
                            };
                            lst.Add(pp);
                        }
                        if (notacredito > 0)
                        {
                            saldo -= notacredito;
                            EntFactura pp = new EntFactura()
                            {
                                //Empresa = p.Empresa,
                                PagoId = 2,
                                Descripcion = "NOTA CRÉDITO",
                                //FormaPago = r["CATFOR_DESCRIPCION"].ToString(),
                                //FechaPago = Convert.ToDateTime(r["NOTCRE_FECHA"]),
                                //FechaCorta = Convert.ToDateTime(r["NOTCRE_FECHA"]).ToShortDateString(),
                                Saldo = saldo,// p.Saldo - pago,
                                Pago = notacredito,
                                Id = Convert.ToInt32(r["NOTCRE_ID"]),//p.Id
                                //NumOrdenRelacion = "NC-" + r["NOTCRE_NUMERO"].ToString(),
                                //ClienteCodigo = p.ClienteCodigo,
                                //Cliente = p.Cliente,
                                //ClienteNombreFiscal = p.ClienteNombreFiscal,
                                SerieFactura = p.SerieFactura,
                                NumeroFactura = p.NumeroFactura,
                                Fecha = p.Fecha,
                                FormaPagoId = p.FormaPagoId,
                                //FormaPago=p.FormaPago,
                                MetodoPagoId = p.MetodoPagoId,
                                //TipoMonedaId = p.TipoMonedaId,
                                //Moneda = p.Moneda,
                                //TipoCambio = p.TipoCambio,
                                IVA = 0,
                                Total = 0,
                                //TotalUSD = p.Total,
                                PagoFactura = p.Total,
                                EstatusId = p.EstatusId,
                                EstatusDescripcion = "",
                                //TipoPedidoId = p.TipoPedidoId,
                                //TipoPedido = "NOTA CRÉDITO | " + r["NOTCRE_DESCRIPCION"].ToString(),
                                //FechaEntrega = Convert.ToDateTime(r["FAC_FECHAREGISTRO"])
                            };
                            lst.Add(pp);
                        }
                    }
                    else //ES PAGO o NC
                    {
                        cont++;
                        EntFactura pp = new EntFactura()
                        {
                            //Empresa = p.Empresa,
                            //Id = p.Id,
                            //ClienteCodigo = p.ClienteCodigo,
                            //Cliente = p.Cliente,
                            //ClienteNombreFiscal = p.ClienteNombreFiscal,
                            SerieFactura = p.SerieFactura,
                            NumeroFactura = p.NumeroFactura,
                            Fecha = p.Fecha,
                            FormaPagoId = p.FormaPagoId,
                            //FormaPago=p.FormaPago,
                            MetodoPagoId = p.MetodoPagoId,
                            //TipoMonedaId = p.TipoMonedaId,
                            //Moneda = p.Moneda,
                            //TipoCambio = p.TipoCambio,
                            IVA = 0,
                            Total = 0,
                            //TotalUSD = p.Total,
                            PagoFactura = p.Total,
                            EstatusId = p.EstatusId,
                            EstatusDescripcion = "",
                            //TipoPedidoId = p.TipoPedidoId,
                            //TipoPedido = "PAGO",
                            //FechaEntrega = Convert.ToDateTime(r["FAC_FECHAREGISTRO"]).AddSeconds(cont)
                        };
                        pago = Convert.ToDecimal(r["PAG_PAGO"]); //Convert.ToDecimal(r["FACING_PAGO"]);                        
                        if (pago != 0)
                        {
                            saldo -= pago;
                            pp.Id = Convert.ToInt32(r["PAG_ID"]);
                            //pp.NumOrdenRelacion = "CP-" + r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                            //pp.NumOrdenRelacion = cp;
                            pp.PagoId = 1;
                            pp.Descripcion = "PAGO";
                            //pp.FormaPago = r["CATFOR_DESCRIPCION"].ToString();
                            //pp.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);
                            //pp.FechaCorta = Convert.ToDateTime(r["PAG_FECHAPAGO"]).ToShortDateString();
                            pp.Saldo = saldo;
                            pp.Pago = pago;
                            //pp.TipoPedido = "PAGO";
                        }
                        else
                        {
                            notacredito = Convert.ToDecimal(r["NOTCRE_MONTO"]);
                            saldo -= notacredito;
                            pp.Id = Convert.ToInt32(r["NOTCRE_ID"]);
                            //pp.NumOrdenRelacion = "NC-" + r["NOTCRE_NUMERO"].ToString();
                            pp.PagoId = 2;
                            pp.Descripcion = "NOTA CRÉDITO";
                            //pp.FormaPago = r["CATFOR_DESCRIPCION"].ToString();
                            //pp.FechaPago = Convert.ToDateTime(r["NOTCRE_FECHA"]);
                            //pp.FechaCorta = Convert.ToDateTime(r["NOTCRE_FECHA"]).ToShortDateString();
                            pp.Saldo = saldo;
                            pp.Pago = notacredito;
                            //pp.TipoPedido = "NOTA CRÉDITO | " + r["NOTCRE_DESCRIPCION"].ToString();
                        }
                        lst.Add(pp);
                    }

                    //lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message + " | " + base.Excepcion); }
        }

        public EntFactura ObtieneUltimaFactura()
        {
            try
            {
                dt = new DatFacturas().obtieneUltimaFactura();
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public EntFactura ObtieneUltimaFactura(int EmpresaId)
        {
            try
            {
                dt = new DatFacturas().obtieneUltimaFactura(EmpresaId);
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// SE USA PARA VERIFICAR SI EL NUMEROFACTURA YA HA SIDO REGISTRADO.
        /// </summary>
        /// <param name="EmpresaId"></param>
        /// <param name="ProveedorId"></param>
        /// <param name="NumeroFactura"></param>
        /// <returns></returns>
        public List<EntFactura> ObtieneFacturasIngresoVigentes(int EmpresaId, int ProveedorId, string NumeroFactura)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                dt = new DatFacturas().obtieneFacturasIngreso(EmpresaId, ProveedorId, NumeroFactura);
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    p.Id = Convert.ToInt32(r["FACING_ID"]);
                    p.EstatusId = Convert.ToInt32(r["FACING_ESTATUSID"]);

                    switch (p.EstatusId)
                    {
                        case 1:
                            p.EstatusDescripcion = "VIGENTE";
                            break;
                        case 2:
                            p.EstatusDescripcion = "PAGADA";
                            break;
                        case 0:
                            p.EstatusDescripcion = "CANCELADA";
                            break;
                    }
                    p.SerieFactura = r["FACING_SERIEFACTURA"].ToString();
                    p.NumeroFactura = r["FACING_NUMEROFACTURA"].ToString();
                    p.UUID = r["FACING_UUID"].ToString();
                    p.Fecha = Convert.ToDateTime(r["FACING_FECHAFACTURA"]);
                    p.MetodoPagoId = Convert.ToInt32(r["FACING_METODOPAGOID"]);
                    p.FormaPagoId = Convert.ToInt32(r["FACING_FORMAPAGOID"]);
                                    
                    p.Ruta = r["FACING_RUTA"].ToString();
                    if (!(r["FACING_PDF"] is DBNull))
                        p.PDF = (byte[])r["FACING_PDF"];
                    if (!(r["FACING_XML"] is DBNull))
                        p.XML = (byte[])r["FACING_XML"];

                    lst.Add(p);
                }
                return lst.Where(P => P.EstatusId > 0).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntFactura> ObtieneComplementos(int FacturaId, string NumeroFactura, decimal Total)
        {
            try
            {
                dt = new DatFacturas().obtieneComplementos(FacturaId);
                List<EntFactura> lst = new List<EntFactura>();
                int numParc = dt.Rows.Count;
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    p.NumeroComplemento = r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                    p.PagoFactura = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);
                    p.PagoId = Convert.ToInt32(r["COMPAG_PAGOID"]);
                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.UUID = r["COMPAG_UUID"].ToString();
                    p.Ruta = r["COMPAG_RUTA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);

                    p.NumeroFactura = NumeroFactura;
                    p.Total = Total;
                    p.PedidoId = numParc;

                    p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);

                    if (p.Estatus)
                        p.Descripcion = "VIGENTE";
                    else
                        p.Descripcion = "CANCELADO";

                    if (!(r["COMPAG_PDF"] is DBNull))
                        p.PDF = (byte[])r["COMPAG_PDF"];
                    else
                        p.PDF = new byte[1];
                    if (!(r["COMPAG_XML"] is DBNull))
                        p.XML = (byte[])r["COMPAG_XML"];
                    else
                        p.XML = new byte[1];

                    p.PedidoId = numParc;

                    numParc--;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntFactura> ObtieneComplementos(int FacturaId)
        {
            try
            {
                dt = new DatFacturas().obtieneComplementosPago(FacturaId);
                List<EntFactura> lst = new List<EntFactura>();
                int numParc = dt.Rows.Count;
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    //p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    p.NumeroComplemento = r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                    p.PagoFactura = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);
                    //p.PagoId = Convert.ToInt32(r["COMPAG_PAGOID"]);
                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.UUID = r["COMPAG_UUID"].ToString();
                    p.Ruta = r["COMPAG_RUTA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);

                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString(); 
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]); 
                    p.PedidoId = numParc;

                    p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);

                    if (p.Estatus)
                        p.Descripcion = "VIGENTE";
                    else
                        p.Descripcion = "CANCELADO";

                    //if (!(r["COMPAG_PDF"] is DBNull))
                    //    p.PDF = (byte[])r["COMPAG_PDF"];
                    //else
                    //    p.PDF = new byte[1];
                    //if (!(r["COMPAG_XML"] is DBNull))
                    //    p.XML = (byte[])r["COMPAG_XML"];
                    //else
                    //    p.XML = new byte[1];

                    p.PedidoId = numParc;

                    numParc--;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public EntFactura ObtieneComplementoPago(string NumeroComplemento, int EmpresaId)
        {
            try
            {
                    EntFactura p = new EntFactura();
                dt = new DatFacturas().obtieneComplementoPago(NumeroComplemento, EmpresaId);
                int numParc = dt.Rows.Count;
                foreach (DataRow r in dt.Rows)
                {
                    //p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    p.NumeroComplemento = r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                    p.PagoFactura = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);
                    //p.PagoId = Convert.ToInt32(r["COMPAG_PAGOID"]);
                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.UUID = r["COMPAG_UUID"].ToString();
                    p.Ruta = r["COMPAG_RUTA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);
                    p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);
                    if (p.Estatus)
                        p.Descripcion = "VIGENTE";
                    else
                        p.Descripcion = "CANCELADO";

                    if (!(r["COMPAG_PDF"] is DBNull))
                        p.PDF = (byte[])r["COMPAG_PDF"];
                    else
                        p.PDF = new byte[1];
                    if (!(r["COMPAG_XML"] is DBNull))
                        p.XML = (byte[])r["COMPAG_XML"];
                    else
                        p.XML = new byte[1];
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntFactura ObtieneUltimoComplementoPago()
        {
            try
            {
                dt = new DatFacturas().obtieneUltimoComplemento();
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    if (p.Id > 0)
                    {
                        p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                        p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);
                    }
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int ObtieneNumeroParcialidades(int FacturaId)
        {
            try
            {
                int numParcialdad = 0;
                dt = new DatFacturas().obtieneNumeroParcialidades(FacturaId);
                //EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    //p.Id = Convert.ToInt32(r["NOTCRE_ID"]);
                    numParcialdad = Convert.ToInt32(r["NUMPAGOS"]);
                }
                //numParcialdad++;
                return numParcialdad;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntFactura ObtieneNotaCredito(int NotaCreditoId)
        {
            try
            {
                EntFactura p = new EntFactura();
                dt = new DatFacturas().obtieneNotaCredito(NotaCreditoId);
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["NOTCRE_ID"]);
                    p.UUID = r["NOTCRE_UUID"].ToString();
                    p.Total = Convert.ToDecimal(r["NOTCRE_CANTIDAD"]);
                    p.Fecha = Convert.ToDateTime(r["NOTCRE_FECHA"]);
                    //p.MetodoPagoId = Convert.ToInt32(r["COMPAG_METODOPAGOID"]);
                    p.FormaPagoId = Convert.ToInt32(r["NOTCRE_FORMAPAGOID"]);
                    p.SerieFactura = r["NOTCRE_SERIE"].ToString();
                    p.NumeroFactura = r["NOTCRE_NUMERO"].ToString();

                    p.Ruta = r["NOTCRE_RUTA"].ToString();
                    if (!(r["NOTCRE_PDF"] is DBNull))
                        p.PDF = (byte[])r["NOTCRE_PDF"];
                    else
                        p.PDF = new byte[1];
                    if (!(r["NOTCRE_XML"] is DBNull))
                        p.XML = (byte[])r["NOTCRE_XML"];
                    else
                        p.XML = new byte[1];

                    p.Estatus = Convert.ToBoolean(r["NOTCRE_ESTATUS"]);
                    if (p.Estatus)
                        p.EstatusDescripcion = "VIGENTE";
                    else
                        p.EstatusDescripcion = "CANCELADA";

                    p.Nombre = r["CLI_NOMBRE"].ToString();

                    p.EmpresaId = Convert.ToInt32(r["NOTCRE_EMPRESAID"]);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaNumeroFactura(EntFactura Factura)
        {
            try
            {
                return new DatFacturas().agregaNumeroFactura(Factura.EmpresaId, Factura.PedidoId, Factura.SerieFactura, Factura.NumeroFactura,1);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaNumeroFactura(int NumeroFacturaId, int FacturaId, string XmlBase64, string PdfBase64)
        {
            try
            {
                new DatFacturas().actualizaNumeroFactura(NumeroFacturaId, FacturaId, XmlBase64, PdfBase64, 2);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaFactura(EntFactura Factura)
        {
            try
            {
                Factura.Id = new DatFacturas().agregaFactura(Factura.EmpresaId, Factura.PedidoId, Factura.SerieFactura, Factura.NumeroFactura,
                                                Factura.UUID,
                                                Factura.TipoComprobanteId, Factura.FormaPagoId, Factura.MetodoPagoId, Factura.UsoCFDIId,
                                                Factura.Fecha, Factura.Ruta, Factura.PDF, Factura.XML, Factura.VersionCFDI);
                //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
                ActualizaNumeroFactura(Factura.SiguienteFacturaId, Factura.Id,
                                                            System.Convert.ToBase64String(Factura.XML),
                                                            System.Convert.ToBase64String(Factura.PDF));
                return Factura.Id;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaFacturaPedidos(EntFactura Factura, int PedidoId)
        {
            try
            {
                return new DatFacturas().agregaFacturaPedidos(Factura.Id, PedidoId, Factura.SerieFactura, Factura.NumeroFactura);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaFacturaIngreso(int EmpresaId, int IngresoId, int ProveedorId, EntFactura Factura)
        {
            try
            {
                return new DatFacturas().agregaFacturaIngreso(EmpresaId, IngresoId, ProveedorId,
                                                                Factura.PedidoId, Factura.TipoComprobanteId,
                                                                Factura.SerieFactura, Factura.NumeroFactura, 
                                                                Factura.IVA, Factura.Total, Factura.Pago, Factura.Fecha,
                                                                Factura.MetodoPagoId, Factura.FormaPagoId, Factura.MonedaId, 
                                                                Factura.TipoCambio,Factura.Descripcion,
                                                                Factura.Ruta, Factura.PDF, Factura.XML,
                                                                Factura.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="Complemento">
        /// Propiedades Necesarias: Id(FacturaId), Fecha, TipoComprobanteId,
        ///                         FormaPagoId, Ruta.
        /// </param>
        public void AgregaComplementePago(int EmpresaId, int PagoId, EntFactura Complemento)
        {
            try
            {
                new DatFacturas().agregaComplementoPago(EmpresaId, Complemento.Id, PagoId, Complemento.Fecha, Complemento.Pago,
                                                        Complemento.TipoComprobanteId, Complemento.FormaPagoId,
                                                        Complemento.NumeroFactura, Complemento.Saldo,
                                                        Complemento.UUID, Complemento.Ruta, Complemento.PDF, Complemento.XML);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void AgregaNotaCredito(EntFactura NotaCredito)
        {
            try
            {
                new DatFacturas().agregaNotaCredito(NotaCredito.EmpresaId, NotaCredito.PedidoId, NotaCredito.Id,
                                                        NotaCredito.TipoComprobanteId, NotaCredito.Descripcion,
                                                        NotaCredito.SerieFactura, NotaCredito.NumeroFactura, NotaCredito.Fecha, NotaCredito.Pago,
                                                        NotaCredito.UUID, NotaCredito.FormaPagoId,
                                                        NotaCredito.Ruta, NotaCredito.PDF, NotaCredito.XML,
                                                        NotaCredito.MonedaId, NotaCredito.TipoCambio, NotaCredito.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizaPDFXMLEnComplementoPago(EntFactura Factura)
        {
            try
            {
                new DatFacturas().actualizaPDFXMLEnComplementoPago(Factura.Id, Factura.Ruta, Factura.PDF, Factura.XML, Factura.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaPDFXMLEnNotaCredito(EntFactura Factura)
        {
            try
            {
                new DatFacturas().actualizaPDFXMLEnNotaCredito(Factura.Id, Factura.Ruta, Factura.PDF, Factura.XML, Factura.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaPagoCompra(EntPago Pago, int FacturaId)
        {
            try
            {
                return new DatFacturas().agregaPagoCompra(Pago.PedidoId, FacturaId, Pago.TipoPagoId, Pago.Cantidad, Pago.FechaPago, 
                                                          Pago.FormaPagoId, Pago.MonedaId, Pago.TipoCambio, Pago.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza Estatus de la Factura.
        /// </summary>
        /// <param name="Factura">
        /// Propiedades Necesarias: Id, EstatusId.
        /// </param>
        public void ActualizaEstatusFacturaPedido(EntFactura Factura)
        {
            try
            {
                new DatFacturas().actualizaEstatusFacturaPedido(Factura.Id, Factura.EstatusId, Factura.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// ES POR NUMERO DE COMPLEMENTO PORQUE SE HACE UN REGISTROS POR CADA PAGO A PEDIDO(AUNQUE SEA UN SOLO COMPLEMENTO)
        /// </summary>
        /// <param name="EmpresaId"></param>
        /// <param name="NumeroComplemento"></param>
        /// <param name="Estatus"></param>
        /// <exception cref="Exception"></exception>
        public void ActualizaEstatusComplementoPago(int EmpresaId, string NumeroComplemento, bool Estatus)
        {
            try
            {
                new DatFacturas().actualizaEstatusComplementoPago(EmpresaId, NumeroComplemento, Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusComplementoPago(int ComplementoPagoId, bool Estatus)
        {
            try
            {
                new DatFacturas().actualizaEstatusComplementoPago(ComplementoPagoId, Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusNotaCredito(EntFactura NotaCredito, int UsuarioId)
        {
            try
            {
                new DatFacturas().actualizaEstatusNotaCredito(NotaCredito.Id, NotaCredito.Estatus, UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
