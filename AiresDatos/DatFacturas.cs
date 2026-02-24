using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatFacturas : DatAbstracta
    {
        public DataTable obtieneFacturasPorPedido(int PedidoId)
        {
            try
            {
                com = new SqlCommand("selObtieneFacturasPorPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneFacturasPedidoConPagos(int EmpresaId, int EstatusId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("[selObtieneFacturasPorEstatusFechasEmpresaConPagos]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneFacturaPedidoConPagos(int FacturaId)
        {
            try
            {
                com = new SqlCommand("[selObtieneFacturaPorEstatusFechasEmpresaConPagos]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneFacturasPedidosPorComplemento(string Complemento, int EmpresaId)
        {
            try
            {
                com = new SqlCommand("[selObtieneFaturasPorComplementosPagoEmpresa]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Complemento", Complemento);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneFacturasPorComplemento(string Complemento, int EmpresaId)
        {
            try
            {
                com = new SqlCommand("[selObtieneFaturasPorComplementoPagoEmpresa]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Complemento", Complemento);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneUltimaFactura()
        {
            try
            {
                com = new SqlCommand("selObtieneUltimaFactura", con);
                com.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneUltimaFactura(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneUltimaFacturaPorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneFacturasIngreso(int EmpresaId, int ProveedorId, string NumeroFactura)
        {
            try
            {
                com = new SqlCommand("[selObtieneFacturasIngresoPorNumeroFacturaProveedor]", con);
                com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
                com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneComplementos(int FacturaId)
        {
            try
            {
                com = new SqlCommand("selObtieneComplementosPagoPorFactura", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneComplementosPago(int FacturaId)
        {
            try
            {
                com = new SqlCommand("selObtieneComplementosPagoPorFacturaNeue", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneComplementoPago(string NumeroComplemento, int EmpresaId)
        {
            try
            {
                com = new SqlCommand("[selObtieneComplementoPagoPorNumeroComplemento]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("NumeroComplemento", NumeroComplemento);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneComplementosPagoPorEmpresa(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneComplementosPagoPorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneComplementosPagoPorEmpresa(int EmpresaId, int EstablecimientoId)
        {
            try
            {
                com = new SqlCommand("selObtieneComplementosPagoPorEmpresaEstablecimiento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneComplementosPagoPorEmpresa(int EmpresaId, int EstablecimientoId,
                                                            DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneComplementosPagoPorEmpresaEstablecimientoFechas", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneComplementosPagoPorEmpresaFechaPago(int EmpresaId, int EstablecimientoId,
                                                            DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneComplementosPagoPorEmpresaEstablecimientoFechaPago", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("FechaPagoDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaPagoHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneUltimoComplemento()
        {
            try
            {
                com = new SqlCommand("selObtieneUltimoComplementoPago", con);
                com.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneNumeroParcialidades(int FacturaId)
        {
            try
            {
                com = new SqlCommand("selObtieneNumeroPagosPorFactura", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneNotaCredito(int NotaCreditoId)
        {
            try
            {
                com = new SqlCommand("[selObtieneNotaCreditoPorId]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("NotaCreditoId", NotaCreditoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneNotasCredito(int EmpresaId, string NumeroNota)
        {
            try
            {
                com = new SqlCommand("[selObtieneNotasCreditoPorEmpresaNumeroNota]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("NumeroNota", NumeroNota);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int agregaNumeroFactura(int EmpresaId, int PedidoId, string SerieFactura, string NumeroFactura, int EstatusId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[insAgregaNumeroFacturaNeue]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("SerieFactura", SerieFactura);
                com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaNumeroFactura(int NumeroFacturaId, int FacturaId, string XmlBase64, string PdfBase64, int EstatusId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[updActualizaNumeroFactura]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("NumeroFacturaId", NumeroFacturaId);
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("XmlBase64", XmlBase64);
                com.Parameters.AddWithValue("PdfBase64", PdfBase64);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                //SqlParameter parm = new SqlParameter("Id", Id);
                //parm.Direction = ParameterDirection.InputOutput;
                //com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaFactura(int EmpresaId, int PedidoId, string SerieFactura, string NumeroFactura, string UUID,
                                    int TipoComprobanteId, int FormaPagoId, int MetodoPagoId, int UsoCFDIId,
                                    DateTime Fecha, string Ruta, byte[] PDF, byte[] XML, string VersionCFDI)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[insAgregaFacturaPedidoNeueVersionSinNF]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("SerieFactura", SerieFactura);
                com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
                //UUID = "";
                com.Parameters.AddWithValue("UUID", UUID);

                com.Parameters.AddWithValue("TipoComprobanteId", TipoComprobanteId);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("MetodoPagoId", MetodoPagoId);
                com.Parameters.AddWithValue("UsoCFDIId", UsoCFDIId);

                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("Ruta", Ruta);
                //PDF = new byte[1];
                //XML = new byte[1];
                com.Parameters.AddWithValue("PDF", PDF);
                com.Parameters.AddWithValue("XML", XML);
                com.Parameters.AddWithValue("VersionCFDI", VersionCFDI);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaFacturaPedidos(int FacturaId, int PedidoId, string SerieFactura, string NumeroFactura)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaFacturaPedidos", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("SerieFactura", SerieFactura);
                com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaFacturaIngreso(int EmpresaId, int IngresoId, int ProveedorId,int PedidoCompraId, int TipoCompraId,
                                        string SerieFactura, string NumeroFactura,
                                        decimal IVAFactura, decimal TotalFactura, decimal Pago, DateTime FechaFactura,
                                        int MetodoPagoId, int FormaPagoId, int MonedaId, decimal TipoCambio, string Descripcion,
                                        string Ruta, byte[] PDF, byte[] XML,
                                        int UsuarioId)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("[insAgregaFacturaIngreso]", con);
                //insAgregaFacturaIngresoConPedidoCompra
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("PedidoCompraId", PedidoCompraId);
                com.Parameters.AddWithValue("TipoCompraId", TipoCompraId);
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
                com.Parameters.AddWithValue("SerieFactura", SerieFactura);
                com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
                com.Parameters.AddWithValue("IEPSFactura", IVAFactura);
                com.Parameters.AddWithValue("IVAFactura", IVAFactura);
                com.Parameters.AddWithValue("TotalFactura", TotalFactura);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaFactura", FechaFactura);
                com.Parameters.AddWithValue("MetodoPagoId", MetodoPagoId);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("MonedaId", MonedaId);
                com.Parameters.AddWithValue("TipoCambio", TipoCambio);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Ruta", Ruta);
                com.Parameters.AddWithValue("PDF", PDF);
                com.Parameters.AddWithValue("XML", XML);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();
                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaComplementoPago(int EmpresaId, int FacturaId, int PagoId, DateTime Fecha, decimal PagoFactura,
                                        int TipoComprobanteId, int FormaPagoId, string NumeroComplemento, decimal Pago, string UUID, 
                                        string Ruta, byte[] PDF, byte[] XML)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[insAgregaComplementoPagoFacturaConEmpresaNeue]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("PagoFactura", PagoFactura);
                com.Parameters.AddWithValue("PagoId", PagoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("NumeroComplemento", NumeroComplemento);
                com.Parameters.AddWithValue("UUID", UUID);
                com.Parameters.AddWithValue("Ruta", Ruta);
                com.Parameters.AddWithValue("PDF", PDF);
                com.Parameters.AddWithValue("XML", XML);
                com.Parameters.AddWithValue("TipoComprobanteId", TipoComprobanteId);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public int agregaNotaCredito(int EmpresaId, int PedidoId, int FacturaId,
                                        int TipoId, string Descripcion,
                                        string Serie, string Numero,
                                        DateTime Fecha, decimal Monto, string UUID,
                                        int FormaPagoId,
                                        string Ruta, byte[] PDF, byte[] XML,
                                        int MonedaId, decimal TipoCambio, int UsuarioId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[insAgregaNotaDeCreditoPedidoNeue]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("TipoId", TipoId);
                Descripcion = "";
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Serie", Serie);
                com.Parameters.AddWithValue("Numero", Numero);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("Monto ", Monto);
                com.Parameters.AddWithValue("UUID", UUID);
                com.Parameters.AddWithValue("Ruta", Ruta);
                com.Parameters.AddWithValue("PDF", PDF);
                com.Parameters.AddWithValue("XML", XML);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("MonedaId", MonedaId);
                com.Parameters.AddWithValue("TipoCambio", TipoCambio);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);

                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaPDFXMLEnComplementoPago(int ComplementoPagoId, string Ruta, byte[] PDF, byte[] XML,
                                                     int UsuarioId)

        {
            try
            {
                com = new SqlCommand("[updActualizaPDFXMLEnComplementoPago]", con);
                //insAgregaFacturaIngresoConPedidoCompra
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ComplementoPagoId", ComplementoPagoId);
                com.Parameters.AddWithValue("RutaArchivos", Ruta);
                com.Parameters.AddWithValue("PDF", PDF);
                com.Parameters.AddWithValue("XML", XML);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaPDFXMLEnNotaCredito(int NotaCreditoId, string Ruta, byte[] PDF, byte[] XML,
                                                     int UsuarioId)

        {
            try
            {
                com = new SqlCommand("[updActualizaPDFXMLEnNotaCredito]", con);
                //insAgregaFacturaIngresoConPedidoCompra
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("NotaCreditoId", NotaCreditoId);
                com.Parameters.AddWithValue("RutaArchivos", Ruta);
                com.Parameters.AddWithValue("PDF", PDF);
                com.Parameters.AddWithValue("XML", XML);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaPagoCompra(int PedidoCompraId, int FacturaIngresoId, int TipoPagoId, decimal Pago, DateTime FechaPago,
                                          int FormaPagoId, int MonedaId, decimal TipoCambio, int UsuarioId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[insAgregaPagoCompra]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoCompraId", PedidoCompraId);
                com.Parameters.AddWithValue("FacturaIngresoId", FacturaIngresoId);
                com.Parameters.AddWithValue("TipoPagoId", TipoPagoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaPago", FechaPago);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("MonedaId", MonedaId);
                com.Parameters.AddWithValue("TipoCambio", TipoCambio);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusFacturaPedido(int FacturaId, int EstatusId, bool ActualizaPedidosFacturado)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusFacturaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                com.Parameters.AddWithValue("ActualizaPedidosFacturado", ActualizaPedidosFacturado);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusComplementoPago(int EmpresaId, string Complemento, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusComplementoPagoEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Complemento", Complemento);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusComplementoPago(int ComplementoId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusComplementoPago", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ComplementoId", ComplementoId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusNotaCredito(int NotaCreditoId, bool Estatus, int UsuarioId)
        {
            try
            {
                com = new SqlCommand("[updActualizaEstatusNotaCreditoConUsuario]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("NotaCreditoId", NotaCreditoId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
    }
}
