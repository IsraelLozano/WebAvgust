using AutoMapper;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;

namespace IDCL.AVGUST.SIP.Manager.Maestro
{
    public class MaestraManager : IMaestraManager
    {
        private readonly IMapper _mapper;
        private readonly MaestraUnitOfWork _maestraUnitOfWork;

        public MaestraManager(IMapper mapper, MaestraUnitOfWork maestraUnitOfWork)
        {
            this._mapper = mapper;
            this._maestraUnitOfWork = maestraUnitOfWork;
        }

        public async Task<List<GetPaisDto>> getListPais()
        {
            var query = _maestraUnitOfWork._paisRepository.GetAll();

            return _mapper.Map<List<GetPaisDto>>(query);
        }
    }
}
