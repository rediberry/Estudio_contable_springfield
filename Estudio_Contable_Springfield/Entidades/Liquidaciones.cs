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
        private int _idLiquidacion;
        private int _idEmpleado;
        private int _periodo;
        private DateTime _fechaAlta;
        private string _codigoTransferencia;
        private double _salarioNeto;
        public Liquidaciones(double bruto, double descuentos, int idempleado, int periodo)
        {
            this._bruto = bruto;
            this._descuentos = descuentos;
            this._idEmpleado = idempleado;
            this._periodo = periodo;
        }
        [DataMember]
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        [DataMember]
        public double Bruto { get => _bruto; set => _bruto = value; }
        [DataMember]
        public double Descuentos { get => _descuentos; set => _descuentos = value; }
        [DataMember (Name = "NumeroLiquidacion")]
        public int IdLiquidacion { get => _idLiquidacion; set => _idLiquidacion = value; }
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
            return String.Format("{0}) Periodo: {1}, Bruto: ${2}, Neto:${3}.",this._idLiquidacion, this._periodo, this._bruto, this.SalarioNeto) ;
        }
    }
}
