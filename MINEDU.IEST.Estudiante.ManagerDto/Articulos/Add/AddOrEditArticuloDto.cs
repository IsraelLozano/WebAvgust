namespace IDCL.AVGUST.SIP.ManagerDto.Articulos.Add
{
    public class AddOrEditArticuloDto
    {
        public int IdArticulo { get; set; }
        public int? IdPais { get; set; }
        public string NombreComercial { get; set; }
        public int? IdTitularRegistro { get; set; }
        public string NroRegistro { get; set; }
        public int? IdTipoProducto { get; set; }
        public int? IdFormulador { get; set; }
        public int? IdGrupoQuimico { get; set; }
        public int? IdTipoFormulacion { get; set; }
        public bool FlgActivo { get; set; }
    }
}
