using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresUtilerias
{
    public abstract class UtiAbstracta
    {
        /// <summary>
        /// Regresa el valor con formato {0:c}.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public string FormatoMoney(decimal Valor)
        {
            return string.Format("{0:c}", Valor);
        }

        /// <summary>
        /// Intenta convertir el Valor solicitado en Decimal y devuelve la conversión. 
        /// Si la conversión es invalida devuelve 0.
        /// Reemplaza "$" a valor si es que lo tiene.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public decimal ConvierteTextoADecimal(string Valor)
        {
            decimal resul = 0;
            Decimal.TryParse(Valor.Replace("$", ""), out resul);
            return resul;
        }

        /// <summary>
        /// Intenta convertir el Valor solicitado en Int y devuelve la conversión. 
        /// Si la conversión es invalida devuelve 0.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public int ConvierteTextoAInteger(string Valor)
        {
            int resul = 0;
            int.TryParse(Valor, out resul);
            return resul;
        }
    }
}
