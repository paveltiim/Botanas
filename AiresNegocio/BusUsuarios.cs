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
    public class BusUsuarios : BusAbstracta
    {
        public List<EntUsuario> ObtieneUsuarios()
        {
            try
            {
                List<EntUsuario> lst = new List<EntUsuario>();
                dt = new DatUsuarios().obtieneUsuarios();
                foreach (DataRow r in dt.Rows)
                {
                    EntUsuario c = new EntUsuario();
                    c.Id = Convert.ToInt32(r["USU_ID"]);
                    c.Descripcion = r["USU_NOMBRE"].ToString();
                    c.Usuario = r["USU_USUARIO"].ToString();
                    c.Contraseña = r["USU_CONTRASEÑA"].ToString();
                    c.EmpresaId = Convert.ToInt32(r["USU_EMPRESAID"]);
                    c.TipoUsuarioId = Convert.ToInt32(r["USU_TIPOUSUARIOID"]);
                    c.Administrador = Convert.ToBoolean(r["USU_ADMINISTRADOR"]);
                    c.Supervisor = Convert.ToBoolean(r["USU_SUPERVISOR"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntUsuario> ObtieneUsuariosSinSistema()
        {
            try
            {
                List<EntUsuario> lst = new List<EntUsuario>();
                dt = new DatUsuarios().obtieneUsuarios();
                foreach (DataRow r in dt.Rows)
                {
                    EntUsuario c = new EntUsuario();
                    c.Id = Convert.ToInt32(r["USU_ID"]);
                    c.Descripcion = r["USU_NOMBRE"].ToString().ToUpper();
                    c.Usuario = r["USU_USUARIO"].ToString();
                    c.Contraseña = r["USU_CONTRASEÑA"].ToString();
                    c.EmpresaId = Convert.ToInt32(r["USU_EMPRESAID"]);
                    c.TipoUsuarioId = Convert.ToInt32(r["USU_TIPOUSUARIOID"]);
                    c.Administrador = Convert.ToBoolean(r["USU_ADMINISTRADOR"]);
                    c.Supervisor = Convert.ToBoolean(r["USU_SUPERVISOR"]);

                    if (c.EmpresaId > 0)
                        lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizaUsuario(EntUsuario Usuario, int UsuarioActualizaId)
        {
            try
            {
                new DatUsuarios().actualizaUsuario(Usuario.Id, Usuario.Descripcion, Usuario.Usuario, Usuario.Contraseña,
                                                        Usuario.Administrador, Usuario.Supervisor, UsuarioActualizaId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
