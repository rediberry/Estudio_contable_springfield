﻿using System;
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
        private int _id;
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
        public int id { get => _id; set => _id = value; }
        [DataMember]
        public string Convenio { get => _convenio; set => _convenio = value; }
        [DataMember]
        public double SueldoBasico { get => _sueldoBasico; set => _sueldoBasico = value; }

        public override string ToString()
        {
            return string.Format("{0} - {1}.",this._convenio,this._nombre);
        }
        public string NombreCompletoCategorias
        {
            get
            {
                return string.Format("{2}) {0} - {1} - Sueldo Básico: ${3}", this._convenio, this._nombre, this.id, this.SueldoBasico.ToString("#,###,###.00"));
            }
        }
    }
}
