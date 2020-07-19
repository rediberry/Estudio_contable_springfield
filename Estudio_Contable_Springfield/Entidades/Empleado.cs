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
        private int _id;
        private long _cuil;
        private DateTime _fechaAlta;
        private int _idEmpresa;
        private int _idCategoria;
        private List<Liquidaciones> _liquidaciones;
        public Empleado(string nombre, string apellido, DateTime fechanac, long cuil, int idempresa, int idcategoria)
        {
            this._nombre = nombre;
            this._apellido = apellido;
            this._fechaNac = fechanac;
            this._cuil = cuil;
            this._idEmpresa = idempresa;
            this._idCategoria = idcategoria;
        }       
        [DataMember]
        public DateTime FechaNacimiento { get => _fechaNac; set => _fechaNac = value; }
        [DataMember]
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        [DataMember]
        public long Cuil { get => _cuil; set => _cuil = value; }
        [DataMember]
        public int id { get => _id; set => _id = value; }
        [DataMember]
        public int IdEmpresa { get => _idEmpresa; set => _idEmpresa = value; }
        [DataMember]
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
            return (this.id == ((Empleado)obj).id);
        }
        
        public int Edad { get { return DateTime.Now.Year - _fechaNac.Year; } }
        public override string ToString()
        {
            return string.Format("{1}, {2} - {3} años - CUIL: {0}.", this._cuil, this._apellido, this._nombre, this.Edad);
        }        
    }
}
