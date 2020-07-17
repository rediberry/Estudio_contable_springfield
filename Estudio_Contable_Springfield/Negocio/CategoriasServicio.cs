using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Entidades;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriasServicio
    {
        private CategoriasMapper mapper;
        public CategoriasServicio()
        {
            mapper = new CategoriasMapper();
        }
        public List<Categorias> TraerListado()
        {
            List<Categorias> listcategorias = mapper.TraerTodos();
            return listcategorias;
        }
        public ResultadoTransaccion AltaCategorias(double sueldobasico, string convenio, string nombre)
        {
            Categorias categorianueva = new Categorias(sueldobasico, convenio, nombre);
            ResultadoTransaccion resultado = mapper.Insert(categorianueva);
            return resultado;
        }
    }
}
