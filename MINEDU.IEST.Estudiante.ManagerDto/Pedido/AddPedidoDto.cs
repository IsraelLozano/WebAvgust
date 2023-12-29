using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Pedido
{
    public class AddPedidoDto: Validation
    {
        //public int IdPedido { get; set; }
        //public int IdEmpresa { get; set; }


        /*
         indCotPed="P";
        "idTipCondicion":1,
          "idFacturar": 0,
        "estado": "P",
         */
        public int IdFacturar { get; set; }
        public string NroRuc { get; set; }
        public int IdLocal { get; set; }
        public int CodPedido { get; set; } = 0;
        public DateTime Fecha { get; set; }       
        public string? IdMoneda { get; set; }
        public string? Observacion { get; set; }
        public string? Indicaciones { get; set; }        
        public string? NroGuia { get; set; }
        public DateTime? FecFactura { get; set; }
        public string? NroFactura { get; set; }
        public int? IdFormaPago { get; set; }
        public int? IdCondicion { get; set; }
        public bool? IndDsctoProntoPago { get; set; }
        public decimal? DsctoProntoPago { get; set; }
        public bool? IndBonificacion { get; set; }
        public bool? IndObsStock { get; set; }
        public string? ObsStock { get; set; }
        public bool? IndObsCredito { get; set; }
        public string? ObsCredito { get; set; }
        public int? IdVendedor { get; set; }        
        public int? IdEstablecimiento { get; set; }        
        public int? IdZona { get; set; }        
        public bool? Tipo { get; set; }
        public decimal? TotsubTotal { get; set; }
        public decimal? TotDscto1 { get; set; }
        public decimal? TotDscto2 { get; set; }
        public decimal? TotDscto3 { get; set; }
        public decimal? TotIsc { get; set; }
        public decimal? TotIgv { get; set; }
        public decimal? TotTotal { get; set; }
        public int? IdSucursalCliente { get; set; }
        public string? PuntoPartida { get; set; }
        public string? PuntoLlegada { get; set; }
        public int? TipoDoc { get; set; }
        public int? IdTransporte { get; set; }
        public int? IdPedidoEnlace { get; set; }
        public string? TipoGeneracion { get; set; }
        public int? IdDivision { get; set; }
        public int? IdCanalVenta { get; set; }
        public decimal? PorDscto { get; set; }
        public bool? FlagListaActivo { get; set; }
        public int? IdListaPrecio { get; set; }
        public bool? CorreoEnviado { get; set; }
        public bool? IndFechaFinReserva { get; set; }
        public DateTime? FechaFinReserva { get; set; }
        public string? UsuarioGen { get; set; }
        public DateTime? FechaGen { get; set; }
        public string? NroGuiaGen { get; set; }
        public string? NroFacturaGen { get; set; }
        public int? Aprobacion { get; set; }
        public string? MensajeAprobacion { get; set; }
        public string? IdMonedaLineaCredito { get; set; }
        public decimal? TipCambio { get; set; }
        public decimal? LineaCredito { get; set; }
        public decimal? CreditoLetras { get; set; }
        public decimal? CreditoFacturas { get; set; }
        public decimal? CreditoDocumento { get; set; }
        public decimal? LineaDisponible { get; set; }
        public string? OrdenDeCompraNum { get; set; }
        public int? DiasValidez { get; set; }
        public int? AprobacionPrecios { get; set; }
        public string? MensajeAprobacionPrecios { get; set; }
        public bool? IndAgencia { get; set; }
        public int? IdAgenciaEnvio { get; set; }
        public int? IdDireccion { get; set; }
        public string? UbigeoCot { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public List<AddPedidoDetalleDto> PedidoDets { get; set; }

    }
}
