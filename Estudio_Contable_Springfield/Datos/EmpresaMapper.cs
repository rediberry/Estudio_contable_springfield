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
    public class EmpresaMapper
    {
        public List<Empresa> TraerTodos()
        {
            string json = WebHelper.Get("api/v1/estudiocontable/Empresas");
            List<Empresa> resultadoMapeo = MapList(json);
            return resultadoMapeo;
        }
        private List<Empresa> MapList(string jsonDeserializar)
        {
            List<Empresa> listDeserealizada = JsonConvert.DeserializeObject<List<Empresa>>(jsonDeserializar);
            return listDeserealizada;
        }
        public ResultadoTransaccion Insert(Empresa empresanueva)
        {
            NameValueCollection obj = ReverseMap(empresanueva);
            string resultadoPost = WebHelper.Post("api/v1/estudiocontable/Empresa", obj);
            ResultadoTransaccion resultado = MapResultado(resultadoPost);
            return resultado;
        }
        private NameValueCollection ReverseMap(Empresa e)
        {
            NameValueCollection n = new NameValueCollection();
            n.Add("RazonSocial", e.RazonSocial);
            n.Add("Cuit", e.Cuit.ToString());
            n.Add("Domicilio", e.Domicilio);            
            return n;
        }
        private ResultadoTransaccion MapResultado(string resultado)
        {
            ResultadoTransaccion mapresult = JsonConvert.DeserializeObject<ResultadoTransaccion>(resultado);
            return mapresult;
        }
        public ResultadoTransaccion Update(Empresa empresaactualizar)
        {
            NameValueCollection obj = ReverseMapUpdate(empresaactualizar);
            string result = WebHelper.Put("api/v1/estudiocontable/Empresa", obj);
            ResultadoTransaccion resultTransaccion = JsonConvert.DeserializeObject<ResultadoTransaccion>(result);
            return resultTransaccion;
        }
        private NameValueCollection ReverseMapUpdate(Empresa e)
        {
            NameValueCollection n = new NameValueCollection();
            n.Add("RazonSocial", e.RazonSocial);
            n.Add("Cuit", e.Cuit.ToString());
            n.Add("Domicilio", e.Domicilio);
            n.Add("id", e.id.ToString());
            return n;
        }
    }
}

