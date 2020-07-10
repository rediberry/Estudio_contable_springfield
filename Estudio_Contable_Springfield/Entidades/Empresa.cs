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
        private int _idEmpresa;
        private List<Empleado> _empleados;
        private string _domicilio;
        private int _cuit;
        public Empresa(int cuit, string razonsocial)
        {
            this._cuit = cuit;
            this._razonSocial = razonsocial;
        }
        [DataMember]
        public DateTime FechaAlta{ get => _fechaAlta; set => _fechaAlta = value; }
        [DataMember]
        public string RazonSocial { get => _razonSocial; set => _razonSocial = value; }
        [DataMember]
        public int IdEmpresa { get => _idEmpresa; set => _idEmpresa = value; }
        [DataMember]
        public List<Empleado> Empleados { get => _empleados; set => _empleados = value; }
        [DataMember]
        public string Domicilio { get => _domicilio; set => _domicilio = value; }
        [DataMember]
        public int Cuit { get => _cuit; set => _cuit = value; }
        public override string ToString()
        {
            return string.Format("{0}) Razon Social: {1} - CUIT: {2}.", this._idEmpresa, this._razonSocial, this._cuit);
        }

    }
}
