using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatUsuarios : DatAbstracta
    {
        public DataTable obtieneUsuarios()
        {
            try
            {
                com = new SqlCommand("selObtieneUsuarios", con);
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
        public void actualizaUsuario(int UsuarioId, string Nombre, string Usuario, string Contraseña,
                                    bool Administrador, bool Supervisor, int UsuarioActualizaId)
        {
            try
            {
                com = new SqlCommand("updActualizaUsuario", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("Usuario", Usuario);
                com.Parameters.AddWithValue("Contraseña", Contraseña);
                com.Parameters.AddWithValue("Administrador", Administrador);
                com.Parameters.AddWithValue("Supervisor", Supervisor);
                com.Parameters.AddWithValue("UsuarioActualizaId", UsuarioActualizaId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

    }
}
