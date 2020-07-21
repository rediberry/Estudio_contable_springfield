﻿using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LiquidacionesServicio
    {
        private LiquidacionesMapper mapper;
        public LiquidacionesServicio()
        {
            mapper = new LiquidacionesMapper();
        }
        public List<Liquidaciones> TraerListado()
        {
            List<Liquidaciones> listliquidaciones = mapper.TraerTodos();
            return listliquidaciones;
        }
        public List<Liquidaciones> TraerListadoPorEmpleado(int idempleado)
        {
            List<Liquidaciones> listliquidacionporempleado = new List<Liquidaciones>();
            List<Liquidaciones> listliquidaciones = mapper.TraerTodos();
            foreach (Liquidaciones l in listliquidaciones)
            {
                if (l.idEmpleado == idempleado)
                {
                    listliquidacionporempleado.Add(l);
                }
            }
            return listliquidacionporempleado;
        }
        
        public ResultadoTransaccion AltaLiquidacion(int idempresa, int periodo, string codigotransferencia, double bruto, double descuentos)
        {
            Liquidaciones liquidacionnueva = new Liquidaciones(idempresa, periodo, codigotransferencia, bruto, descuentos);
            ResultadoTransaccion resultado = mapper.Insert(liquidacionnueva);
            return resultado;
        }     
    }
}
