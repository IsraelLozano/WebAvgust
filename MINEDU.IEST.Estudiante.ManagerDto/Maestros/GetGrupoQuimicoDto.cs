using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.ManagerDto.Maestros
{
    public class GetGrupoQuimicoDto: Validation
    {
        public int IdGrupoQuimico { get; set; }
        public string NomGrupoQuimico { get; set; }
        public bool estado { get; set; }
    }
}
