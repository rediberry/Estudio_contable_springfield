using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{    
    public abstract class Persona
    {
        protected string _nombre;
        protected string _apellido;
        protected string _direccion;
        protected string _telefono;
        protected string _mail;

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string telefono { get => _telefono; set => _telefono = value; }
        public string mail { get => _mail; set => _mail = value; }
        
        public virtual string GetNombreCompleto()
        {
            return string.Format("{0}, {1}", this._apellido, this._nombre);
        }
    }
}
