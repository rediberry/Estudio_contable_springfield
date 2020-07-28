using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [DataContract]
    public class Empresa
    {
        private DateTime _fechaAlta;
        private string _razonSocial;
        private int _id;
        private List<Empleado> _empleados;
        private string _domicilio;
        private long _cuit;
        public Empresa(string razonsocial, long cuit, string domicilio)
        {            
            this._razonSocial = razonsocial;
            this._cuit = cuit;
            this._domicilio = domicilio;            
        }
        [DataMember]
        public DateTime FechaAlta{ get => _fechaAlta; set => _fechaAlta = value; }
        [DataMember]
        public string RazonSocial { get => _razonSocial; set => _razonSocial = value; }
        [DataMember]
        public int id { get => _id; set => _id = value; }
        [DataMember]
        public List<Empleado> Empleados { get => _empleados; set => _empleados = value; }
        [DataMember]
        public string Domicilio { get => _domicilio; set => _domicilio = value; }
        [DataMember]
        public long Cuit { get => _cuit; set => _cuit = value; }
        public override string ToString()
        {
            return string.Format("{0}) {1} - CUIT: {2}.", this._id, this._razonSocial, this._cuit);
        }
        public string NombreCompletoEmpresa
        {
            get
            {
                return string.Format("{0}) {1} - CUIT: {2} - Fecha Alta: {3} - Domicilio: {4}.", this._id, this._razonSocial, this._cuit, this.FechaAlta.ToString("dd/MM/yyyy"), this.Domicilio);
            }
        }
        public string NombreAbreviadoEmpresa
        {
            get
            {
                return string.Format("{0}) {1}", this._id, this._razonSocial);
            }
        }
    }
}
