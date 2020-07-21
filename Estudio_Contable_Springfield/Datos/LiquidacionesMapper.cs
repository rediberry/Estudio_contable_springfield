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
            string json = WebHelper.Get("api/v1/estudiocontable/Liquidaciones");
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
            n.Add("idEmpresa", l.idEmpresa.ToString());
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
    }
}

