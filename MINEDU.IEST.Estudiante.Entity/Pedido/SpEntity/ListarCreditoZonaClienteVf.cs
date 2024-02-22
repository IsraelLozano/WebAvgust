using Newtonsoft.Json;

namespace IDCL.AVGUST.SIP.Entity.Pedido.SpEntity
{
    public class ListarCreditoZonaClienteVf
    {
        
        public long idZona { get; set; }

        
        public string Zona { get; set; }

        
        public string Cliente { get; set; }

        
        public double LineaCredito { get; set; }

        
        public double Disponible { get; set; }

        
        public long TotalVencidos { get; set; }

        
        public long PorAceptar { get; set; }
    }
}
