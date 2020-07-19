using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class EmpresaServicio
    {
        private EmpresaMapper mapper;
        public EmpresaServicio()
        {
            mapper = new EmpresaMapper();
        }
        public List<Empresa> TraerListado()
        {
            List<Empresa> listempresa = mapper.TraerTodos();
            return listempresa;
        }
        public ResultadoTransaccion AltaEmpresa(string razonsocial, long cuit, string domicilio)
        {
            Empresa empresanueva = new Empresa(razonsocial, cuit, domicilio);
            ResultadoTransaccion resultado = mapper.Insert(empresanueva);
            return resultado;
        }
    }
}
