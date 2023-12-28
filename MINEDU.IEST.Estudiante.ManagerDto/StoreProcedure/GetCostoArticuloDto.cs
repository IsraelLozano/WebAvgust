namespace IDCL.AVGUST.SIP.ManagerDto.StoreProcedure
{
    public class GetCostoArticuloDto
    {
        public long Anio { get; set; }
        public long Mes { get; set; }
        public long IdArticulo { get; set; }
        public string CodArticulo { get; set; }
        public string NomArticulo { get; set; }
        public double CostoUnit { get; set; }
        public string Categoria { get; set; }
        public double Porcentaje { get; set; }
        public double StockDisponible { get; set; }
    }
}
