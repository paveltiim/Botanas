using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresNegocio
{
    public abstract class BusAbstracta
    {
        protected DataTable dt;

        protected int ErrorId = 0;

        protected string EscribeArray(DataColumnCollection Columnas, object[] Array)
        {
            int col = 0;
            StringBuilder sb = new StringBuilder();
            foreach (object o in Array)
            {
                sb.Append(Columnas[col].ColumnName);
                sb.Append(": ");
                sb.AppendLine(o.ToString());
                col++;
            }
            return sb.ToString();
        }
        protected string Excepcion { get { return ErrorId.ToString() + " " + EscribeArray(dt.Columns, dt.Select(dt.Columns[0].ColumnName + "=" + ErrorId.ToString()).First().ItemArray); } }

    }
}
