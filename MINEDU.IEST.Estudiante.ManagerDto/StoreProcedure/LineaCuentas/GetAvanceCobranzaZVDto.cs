namespace IDCL.AVGUST.SIP.ManagerDto.StoreProcedure.LineaCuentas
{
    public class GetAvanceCobranzaZVDto
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
