namespace IDCL.AVGUST.SIP.Entity.Pedido.SpEntity
{
    public class AvanceCobranzaZV
    {
        public long AnioPresupuesto { get; set; }
        public long Mes { get; set; }
        public long idEstablecimiento { get; set; }
        public string desEstablecimiento { get; set; }
        public string nomVendedor { get; set; }
        public double totCuota { get; set; }
        public double Avance { get; set; }
    }
}
