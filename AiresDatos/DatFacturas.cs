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
        //MOVER A DatFactura
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
        public DataTable obtieneFacturasPorComplemento(string Complemento)
        {
            try
            {
                com = new SqlCommand("[selObtieneFaturasPorComplementosPago]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Complemento", Complemento);
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

        public int agregaFactura(int EmpresaId, int PedidoId, string SerieFactura, string NumeroFactura, string UUID,
                                    int TipoComprobanteId, int FormaPagoId, int MetodoPagoId, int UsoCFDIId,
                                    DateTime Fecha, string Ruta)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaFacturaPedido33", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("SerieFactura", SerieFactura);
                com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
                com.Parameters.AddWithValue("UUID", UUID);

                com.Parameters.AddWithValue("TipoComprobanteId", TipoComprobanteId);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("MetodoPagoId", MetodoPagoId);
                com.Parameters.AddWithValue("UsoCFDIId", UsoCFDIId);

                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("Ruta", Ruta);
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

        public int agregaComplementoPago(int FacturaId, DateTime Fecha, decimal PagoFactura,
                                        int TipoComprobanteId, int FormaPagoId, string NumeroComplemento, decimal Pago, string UUID, string Ruta)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaComplementoPagoFactura", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("PagoFactura", PagoFactura);
                com.Parameters.AddWithValue("TipoComprobanteId", TipoComprobanteId);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("NumeroComplemento", NumeroComplemento);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("UUID", UUID);
                com.Parameters.AddWithValue("Ruta", Ruta);
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

        public void actualizaEstatusFacturaPedido(int FacturaId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusFacturaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusComplementoPago(int ComplementoPagoId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusComplementoPago", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Complemento", ComplementoPagoId.ToString());
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
    }
}
