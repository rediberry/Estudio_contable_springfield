using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class LiquidacionesMapper
    {
        public List<Liquidaciones> TraerTodos()
        {
            string json = WebHelper.Get("api/v1/estudiocontable/liquidaciones");
            List<Liquidaciones> resultadoMapeo = MapList(json);
            return resultadoMapeo;
        }
        private List<Liquidaciones> MapList(string jsonDeserializar)
        {
            List<Liquidaciones> listDeserealizada = JsonConvert.DeserializeObject<List<Liquidaciones>>(jsonDeserializar);
            return listDeserealizada;
        }
        public ResultadoTransaccion Insert(Liquidaciones liquidacionnueva)
        {
            NameValueCollection obj = ReverseMap(liquidacionnueva);
            string resultadoPost = WebHelper.Post("api/v1/estudiocontable/Liquidacion", obj);
            ResultadoTransaccion resultado = MapResultado(resultadoPost);
            return resultado;
        }
        private NameValueCollection ReverseMap(Liquidaciones l)
        {
            NameValueCollection n = new NameValueCollection();
            n.Add("idEmpleado", l.idEmpleado.ToString());
            n.Add("Periodo", l.Periodo.ToString());
            n.Add("CodigoTransferencia", l.CodigoTransferencia);
            n.Add("Bruto", l.Bruto.ToString());
            n.Add("Descuentos", l.Descuentos.ToString());          
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
            string resultadoPost = WebHelper.Delete("api/v1/estudiocontable/Liquidacion/" + id.ToString(), obj);
            ResultadoTransaccion resultado = MapResultado(resultadoPost);
            return resultado;
        }
        private NameValueCollection ReverseMapDelete(int id)
        {
            NameValueCollection n = new NameValueCollection();
            n.Add("codigo", id.ToString());
            return n;
        }
        public ResultadoTransaccion Update(Liquidaciones liquidacionaactualizar)
        {
            NameValueCollection obj = ReverseMapUpdate(liquidacionaactualizar);
            string result = WebHelper.Put("api/v1/estudiocontable/Liquidacion", obj);
            ResultadoTransaccion resultTransaccion = JsonConvert.DeserializeObject<ResultadoTransaccion>(result);
            return resultTransaccion;
        }
        private NameValueCollection ReverseMapUpdate(Liquidaciones l)
        {
            NameValueCollection n = new NameValueCollection();            
            n.Add("idEmpleado", l.idEmpleado.ToString());
            n.Add("Periodo", l.Periodo.ToString());
            n.Add("CodigoTransferencia", l.CodigoTransferencia);
            n.Add("Bruto", l.Bruto.ToString());
            n.Add("Descuentos", l.Descuentos.ToString());
            n.Add("id", l.id.ToString());
            return n;
        }
    }
}

