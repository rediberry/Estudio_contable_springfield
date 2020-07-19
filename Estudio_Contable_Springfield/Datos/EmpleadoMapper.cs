using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace Datos
{
    public class EmpleadoMapper
    {
        public List<Empleado> TraerTodos()
        {
            string json = WebHelper.Get("api/v1/estudiocontable/Empleados");
            List<Empleado> resultadoMapeo = MapList(json);
            return resultadoMapeo;
        }
        private List<Empleado> MapList(string jsonDeserializar)
        {
            List<Empleado> listDeserealizada = JsonConvert.DeserializeObject<List<Empleado>>(jsonDeserializar);
            return listDeserealizada;
        }
        public ResultadoTransaccion Insert(Empleado empleadonuevo)
        {
            NameValueCollection obj = ReverseMap(empleadonuevo);
            string resultadoPost = WebHelper.Post("api/v1/estudiocontable/Empleado", obj);
            ResultadoTransaccion resultado = MapResultado(resultadoPost);
            return resultado;
        }
        private NameValueCollection ReverseMap(Empleado e)
        {
            NameValueCollection n = new NameValueCollection();
            n.Add("idCategoria", e.IdCategoria.ToString());
            n.Add("idEmpresa", e.IdEmpresa.ToString());
            n.Add("Cuil", e.Cuil.ToString());
            n.Add("Nombre", e.Nombre.ToString());
            n.Add("Apellido", e.Apellido.ToString());
            n.Add("FechaNacimiento", e.FechaNacimiento.ToString());
            return n;
        }
        private ResultadoTransaccion MapResultado(string resultado)
        {
            ResultadoTransaccion mapresult = JsonConvert.DeserializeObject<ResultadoTransaccion>(resultado);
            return mapresult;
        }
        public ResultadoTransaccion Delete(int id)
        {
            NameValueCollection obj = ReverseMapDelete(id);
            string resultadoPost = WebHelper.Delete("api/v1/estudiocontable/Empleado/"+id.ToString(), obj);
            ResultadoTransaccion resultado = MapResultado(resultadoPost);
            return resultado;
        }
        private NameValueCollection ReverseMapDelete(int id)
        {
            NameValueCollection n = new NameValueCollection();
            n.Add("codigo", id.ToString());            
            return n;
        }        
    }
}
