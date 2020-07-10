using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class ValidacionHelper
    {
        public static string ValidarString(string palabra)
        {
            int comprobacion = 0;
            if (int.TryParse(palabra, out comprobacion))
            {
                return "";
            }
            else
            {
                return palabra;
            }
        }
        public static int ValidarInt(string numero)
        {
            int comprobacion = 0;
            if (!int.TryParse(numero, out comprobacion))
            {
                return -1;
            }
            else
            {
                return comprobacion;
            }
        }
        public static double ValidarDouble(string numero)
        {
            double comprobacion = 0;
            if (!double.TryParse(numero, out comprobacion))
            {
                return -1;
            }
            else
            {
                return comprobacion;
            }
        }
    }
}
