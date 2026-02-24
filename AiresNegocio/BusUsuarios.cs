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
                    c.CompañiaId = Convert.ToInt32(r["USU_EMPRESAID"]);//EN BD ESTA ES LA COMPAÑIAID
                    c.EmpresaId = Convert.ToInt32(r["USU_EMPRESAID"]);
                    c.TipoUsuarioId = Convert.ToInt32(r["USU_TIPOUSUARIOID"]);
                    c.Administrador = Convert.ToBoolean(r["USU_ADMINISTRADOR"]);
                    c.Supervisor = Convert.ToBoolean(r["USU_SUPERVISOR"]);
                    c.AlmacenMayoristaId = Convert.ToInt32(r["USU_ALMACENMAYORISTAID"]); 
                    c.EstablecimientoCajaId = Convert.ToInt32(r["USU_ESTABLECIMIENTOCAJAID"]);
                    c.EstablecimientoClientesId= Convert.ToInt32(r["USU_ESTABLECIMIENTOCLIENTESID"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
    }
}
