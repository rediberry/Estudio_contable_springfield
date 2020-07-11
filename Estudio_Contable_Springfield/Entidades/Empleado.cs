using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [DataContract]
    public class Empleado : Persona
    {
        private DateTime _fechaNac;
        private int _idEmpleado;
        private int _cuil;
        private DateTime _fechaAlta;
        private int _idEmpresa;
        private int _idCategoria;
        private List<Liquidaciones> _liquidaciones;
        public Empleado(string nombre, string apellido, DateTime fechanac, int cuil, int idempresa, int idcategoria)
        {
            this._nombre = nombre;
            this._apellido = apellido;
            this._fechaNac = fechanac;
            this._cuil = cuil;
            this._idEmpresa = idempresa;
            this._idCategoria = idcategoria;
        }
        [DataMember]
        public DateTime FechaNac { get => _fechaNac; set => _fechaNac = value; }
        [DataMember]
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        [DataMember]
        public int Cuil { get => _cuil; set => _cuil = value; }
        [DataMember(Name = "Legajo")]
        public int IdEmpleado { get => _idEmpleado; set => _idEmpleado = value; }
        [DataMember(Name = "CodigoEmpresa")]
        public int IdEmpresa { get => _idEmpresa; set => _idEmpresa = value; }
        [DataMember(Name = "Categoria")]
        public int IdCategoria { get => _idCategoria; set => _idCategoria = value; }
        [DataMember]
        public List<Liquidaciones> Liquidaciones { get => _liquidaciones; set => _liquidaciones = value; }
        public override bool Equals(object obj)
        {
            // If the passed object is null
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Empleado))
            {
                return false;
            }
            return (this.IdEmpleado == ((Empleado)obj).IdEmpleado);
        }
        // la realiadad es que esta validación se debería hacer al construir el objeto, pero 
        // para explicar el if inline y una validación de fecha nos tomamos esta licencia
        public int Edad { get => DateTime.Now.Year > 1900 ? (DateTime.Now.Year - _fechaNac.Year) : throw new Exception("Nadie tiene mas de 120 años"); }
        public override string ToString()
        {
            return string.Format("{0}) fecha alta: {1} - CUIL: {2}.",this._idEmpleado, this._fechaAlta, this._cuil);
        }
    }
}
