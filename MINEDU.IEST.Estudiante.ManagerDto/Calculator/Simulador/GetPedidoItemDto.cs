namespace IDCL.AVGUST.SIP.ManagerDto.Calculator.Simulador
{
    public class GetPedidoItemDto
    {
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public string Codigo { get; set; } = null!;
        public string Producto { get; set; } = null!;
        public string CodigoFamilia { get; set; } = null!;
        public string Familia { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioVvd { get; set; }
        public decimal Importe { get; set; }
        public decimal Costo { get; set; }
        public decimal Mb { get; set; }
        public decimal PartImporteTotal { get; set; }
        public decimal PesoAsignadoPercent { get; set; }
        public decimal ComisionPercent { get; set; }
    }
}
