using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Datos;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmpleadoServicio
    {
        private EmpleadoMapper mapper;
        public EmpleadoServicio()
        {
            mapper = new EmpleadoMapper();
        }
        public List<Empleado> TraerListado()
        {
            List<Empleado> listempresa = mapper.TraerTodos();
            return listempresa;
        }
        public List<Empleado> TraerListadoPorEmpresa(int idempresa)
        {
            List<Empleado> listempleadoporempresa = new List<Empleado>();
            List<Empleado> listempresa = mapper.TraerTodos();
            foreach (Empleado e in listempresa)
            {
                if (e.IdEmpresa == idempresa)
                {
                    listempleadoporempresa.Add(e);
                }
            }
            return listempleadoporempresa;
        }
        public Empleado ObtenerEmpleado(int idempleado)
        {
            Empleado empleado = new Empleado("", "", DateTime.Now, 2033333330, 1, 1);
            List<Empleado> listempresa = mapper.TraerTodos();
            foreach (Empleado e in listempresa)
            {
                if (e.id == idempleado)
                {
                    empleado = e;
                    break;
                }
            }
            return empleado;
        }
        public ResultadoTransaccion AltaEmpleado(string nombre, string apellido, DateTime fechanac, long cuil, int idempresa, int idcategoria)
        {
            Empleado empleadonuevo = new Empleado(nombre, apellido, fechanac, cuil, idempresa, idcategoria);
            ResultadoTransaccion resultado = mapper.Insert(empleadonuevo);
            return resultado;
        }
        public ResultadoTransaccion EliminarEmpleado(int id)
        {            
            ResultadoTransaccion resultado = mapper.Delete(id);
            return resultado;
        }
        public ResultadoTransaccion ModificarEmpleado(string nombre, string apellido, DateTime fechanac, long cuil, int idempresa, int idcategoria, int idempleado)
        {
            Empleado empleadoactualizar = new Empleado(nombre, apellido, fechanac, cuil, idempresa, idcategoria);
            empleadoactualizar.id = idempleado;
            ResultadoTransaccion resultado = mapper.Update(empleadoactualizar);
            return resultado;
        }
    }
}
