using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.ManagerDto.Articulos
{
    public class GetArticuloShortDto
    {
        public int IdArticulo { get; set; }
        public string NombreComercial { get; set; }
        public GetTitularRegistroDto IdTitularRegistroNavigation { get; set; }
        public GetPaisDto IdPaisNavigation { get; set; }
    }
}
