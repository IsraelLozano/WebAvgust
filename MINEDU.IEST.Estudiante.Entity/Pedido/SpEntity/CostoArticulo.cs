using Newtonsoft.Json;

namespace IDCL.AVGUST.SIP.Entity.Pedido.SpEntity
{
    public class CostoArticulo
    {
        [JsonProperty("anio")]
        public long Anio { get; set; }

        [JsonProperty("mes")]
        public long Mes { get; set; }

        [JsonProperty("idArticulo")]
        public long IdArticulo { get; set; }

        [JsonProperty("codArticulo")]
        public string CodArticulo { get; set; }

        [JsonProperty("nomArticulo")]
        public string NomArticulo { get; set; }

        [JsonProperty("CostoUnit")]
        public double CostoUnit { get; set; }

        [JsonProperty("Categoria")]
        public string Categoria { get; set; }

        [JsonProperty("Porcentaje")]
        public double Porcentaje { get; set; }
        [JsonProperty("StockDisponible")]
        public double StockDisponible { get; set; }
    }
}
