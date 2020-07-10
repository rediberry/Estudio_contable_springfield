using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEntidades
{
    [DataContract]
    public class ResultadoTransaccion
    {
        [DataMember]
        public bool IsOk { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Error { get; set; }
    }
}
