using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public abstract class DatAbstracta
    {
        public SqlConnection con;
        public SqlCommand com;
        public SqlDataAdapter da;
        public DataTable dt;
        public int cantidadRenglones;

        public DatAbstracta()
        {
            //string cadena = ConfigurationManager.ConnectionStrings["SQLPromo"].ToString();
            string cadena = ConfigurationManager.ConnectionStrings["SQLSerdan"].ToString();
            cadena = "Data Source=tiimserver.database.windows.net;Initial Catalog=BotanasAzure;User ID=tiim;password =Zoeserv10";
            con = new SqlConnection(cadena);
        }
    }
}
