using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace Datos
{
    public class CategoriasMapper
    {
        public List<Categorias> TraerTodos()
        {
            string json = WebHelper.Get("api/v1/estudiocontable/Categorias");
            List<Categorias> resultadoMapeo = MapList(json);
            return resultadoMapeo;
        }
        private List<Categorias> MapList(string jsonDeserializar)
        {
            List<Categorias> listDeserealizada = JsonConvert.DeserializeObject<List<Categorias>>(jsonDeserializar);
            return listDeserealizada;
        }
        public ResultadoTransaccion Insert(Categorias plazofijonuevo)
        {
            NameValueCollection obj = ReverseMap(plazofijonuevo);
            string resultadoPost = WebHelper.Post("api/v1/estudiocontable/Categoria", obj);
            ResultadoTransaccion resultado = MapResultado(resultadoPost);
            return resultado;
        }
        private NameValueCollection ReverseMap(Categorias c)
        {
            NameValueCollection n = new NameValueCollection();
            n.Add("Nombre", c.Nombre);
            n.Add("Convenio", c.Convenio);
            n.Add("SueldoBasico", c.SueldoBasico.ToString());            
            return n;
        }
        private ResultadoTransaccion MapResultado(string resultado)
        {
            ResultadoTransaccion mapresult = JsonConvert.DeserializeObject<ResultadoTransaccion>(resultado);
            return mapresult;
        }
    }
}
