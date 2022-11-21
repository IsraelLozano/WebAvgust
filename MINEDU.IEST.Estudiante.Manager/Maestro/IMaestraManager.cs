using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.Manager.Maestro
{
    public interface IMaestraManager
    {
        Task<List<GetPaisDto>> getListPais();
    }
}