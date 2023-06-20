using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetIdTipoProductoDto: Validation
    {
        public int IdTipoProducto1 { get; set; }
        public string NomTipoProducto { get; set; }
        public bool estado { get; set; }
    }
}
