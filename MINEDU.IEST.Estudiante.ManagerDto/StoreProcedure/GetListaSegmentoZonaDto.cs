namespace IDCL.AVGUST.SIP.ManagerDto.StoreProcedure
{
    public class GetListaSegmentoZonaDto
    {
        public long Anio { get; set; }
        public long Mes { get; set; }
        public string desEstablecimiento { get; set; }
        public string desZona { get; set; }
        public string nomVendedor { get; set; }
        public string CategoriaReg { get; set; }
        public double subTotal { get; set; }
        public double totalVenta { get; set; }
    }
}
