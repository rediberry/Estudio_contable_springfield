using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [DataContract]
    public class Categorias
    {
        private string _nombre;
        private int _idCategoria;
        private string _convenio;
        private double _sueldoBasico;
        public Categorias(double sueldobasico, string convenio, string nombre)
        {
            this._convenio = convenio;
            this._sueldoBasico = sueldobasico;
            this._nombre = nombre;
        }
        [DataMember]
        public string Nombre { get => _nombre; set => _nombre = value; }
        [DataMember]
        public int IdCategoria { get => _idCategoria; set => _idCategoria = value; }
        [DataMember]
        public string Convenio { get => _convenio; set => _convenio = value; }
        [DataMember]
        public double SueldoBasico { get => _sueldoBasico; set => _sueldoBasico = value; }

        public override string ToString()
        {
            return string.Format("Convenio{0} - {1}) Nombre:{2} - Sueldo Básico{3}.",this._convenio,this._idCategoria, this._nombre, this._sueldoBasico);
        }


    }
}
