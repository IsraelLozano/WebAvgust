namespace IDCL.AVGUST.SIP.ManagerDto.Pedido
{
    public class AddPedidoDetalleDto
    {
        public int IdItem { get; set; }
        public bool? IndArticuloNuevo { get; set; }
        public int? IdArticulo { get; set; }
        public string? NomArticulo { get; set; }
        public bool? FlgAfectacionIgv { get; set; }
        public string? TipoAfectacionIgv { get; set; }
        public int? IdTipoPrecio { get; set; }
        public bool? IndSinStock { get; set; }
        public decimal Cantidad { get; set; }
        public int? CantidadUnit { get; set; }
        public decimal? CantidadFinal { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal? PrecioConImpuesto { get; set; }
        public decimal? Dscto1 { get; set; }
        public decimal? Dscto2 { get; set; }
        public decimal? Dscto3 { get; set; }
        public decimal? PorDscto1 { get; set; }
        public decimal? PorDscto2 { get; set; }
        public decimal? PorDscto3 { get; set; }
        public bool? FlgIgv { get; set; }
        public decimal? Igv { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }
        public decimal? PorIgv { get; set; }
        public int? IdTipoMedida { get; set; }
        public int? IdUmedida { get; set; }
        public int? IdTipoArticulo { get; set; }
        public int? IdAlmacen { get; set; }
        public decimal? Stock { get; set; }
        public string? Lote { get; set; }
        public string? NroOt { get; set; }
        public bool? IndCalculo { get; set; }
        public string? TipoImpSelectivo { get; set; }
        public decimal? Capacidad { get; set; }
        public decimal? Contenido { get; set; }
        public bool? IndDetraccion { get; set; }
        public string? TipDetraccion { get; set; }
        public decimal? TasaDetraccion { get; set; }
        public bool? IndPrecioUnit { get; set; }
        public decimal? PrecioUnitIni { get; set; }
        public string? TipArticulo { get; set; }
        public bool? IndDistribuirLote { get; set; }
        public bool? IndNoAtender { get; set; }
        public int? ItemPrecio { get; set; }
        public bool? VerImagen { get; set; }
        public bool? IndUmedida { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
