using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [DataContract]
    public class Liquidaciones
    {
        private double _bruto;
        private double _descuentos;
        private int _id;        
        private int _idEmpleado;
        private int _periodo;
        private DateTime _fechaAlta;
        private string _codigoTransferencia;
        private double _salarioNeto;
        public Liquidaciones(int idempleado, int periodo, string codigotransferencia, double bruto, double descuentos)
        {
            this._bruto = bruto;
            this._descuentos = descuentos;            
            this._idEmpleado = idempleado;
            this._periodo = periodo;
            this._codigoTransferencia = codigotransferencia;
        }
        [DataMember]
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        [DataMember]
        public double Bruto { get => _bruto; set => _bruto = value; }
        [DataMember]
        public double Descuentos { get => _descuentos; set => _descuentos = value; }
        [DataMember]
        public int id { get => _id; set => _id = value; }        
        [DataMember]
        public int idEmpleado { get => _idEmpleado; set => _idEmpleado = value; }
        [DataMember]
        public int Periodo { get => _periodo; set => _periodo = value; }
        [DataMember]
        public string CodigoTransferencia { get => _codigoTransferencia; set => _codigoTransferencia = value; }
        [DataMember]
        public double SalarioNeto { get => (_bruto - _descuentos); set => _salarioNeto = value; }
        public override string ToString()
        {
            return String.Format("{0}) Bruto: ${1}, Neto:${2}, Fecha Liq. {3}.", this._periodo.ToString("####-##"), this._bruto.ToString("#,###,###.00"), this.SalarioNeto.ToString("#,###,###.00"), this.FechaAlta.ToString("dd/MM/yyyy")) ;
        }
    }
}
