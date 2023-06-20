namespace IDCL.AVGUST.SIP.ManagerDto.Calculator.Simulador
{
    public class GetPedidoDto
    {
        public int IdPedido { get; set; }
        public DateTime FechaOperacion { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal VentaTotal { get; set; }
        public decimal ComisionPercent { get; set; }
        public decimal ComisionMonto { get; set; }

        public List<GetPedidoItemDto> SimuladorPedidoItems { get; set; }
    }
}
