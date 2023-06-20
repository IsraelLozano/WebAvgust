using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetClaseDto: Validation
    {
        public int IdClase { get; set; }
        public string Descripcion { get; set; }
        public bool estado { get; set; }
        public int? IdTipoProducto { get; set; }
        public GetIdTipoProductoDto IdTipoProductoNavigation { get; set; }
    }
}
