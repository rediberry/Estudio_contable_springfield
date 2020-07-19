using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public static class ValidacionHelper
    {
        public static string ValidarStringNombre(string palabra)
        {            
            if (Regex.IsMatch(palabra, @"^[a-zA-Z]+$"))
            {
                return palabra;
            }
            else
            {
                return "";
            }
        }
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
        public static Boolean ValidarDatetime(String fecha)
        {
            try
            {                            
                int age = GetAge(DateTime.Parse(fecha));
                if (age < 18 || age > 80)
                {
                    return false;                    
                }
                return true;                
            }
            catch
            {
                return false;
            }
        }
        public static int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birthDate.Year;
            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day)) { age--; }
            return age;
        }
        public static bool ValidarCuit(string cuit)
        {
            if (string.IsNullOrEmpty(cuit)) throw new ArgumentNullException(nameof(cuit),"Debe ingresar un CUIT/CUIL válido");
            if (cuit.Length != 11) throw new ArgumentException(nameof(cuit), "Debe ingresar un CUIT/CUIL válido");
            bool rv = false;
            int verificador;
            int resultado = 0;
            string cuit_nro = cuit.Replace("-", string.Empty);
            string codes = "6789456789";
            long cuit_long = 0;
            if (long.TryParse(cuit_nro, out cuit_long))
            {
                verificador = int.Parse(cuit_nro[cuit_nro.Length - 1].ToString());
                int x = 0;
                while (x < 10)
                {
                    int digitoValidador = int.Parse(codes.Substring((x), 1));
                    int digito = int.Parse(cuit_nro.Substring((x), 1));
                    int digitoValidacion = digitoValidador * digito;
                    resultado += digitoValidacion;
                    x++;
                }
                resultado = resultado % 11;
                rv = (resultado == verificador);
            }
            return rv;
        }        
    }
}
