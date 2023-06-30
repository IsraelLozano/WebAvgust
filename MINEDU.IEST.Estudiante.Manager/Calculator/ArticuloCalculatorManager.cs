using AutoMapper;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloCalc;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;

namespace IDCL.AVGUST.SIP.Manager.Calculator
{
    public class ArticuloCalculatorManager : IArticuloCalculatorManager
    {
        private readonly IMapper _mapper;
        private readonly CalculatorUnitOfWork _calculatorUnitOfWork;

        public ArticuloCalculatorManager(IMapper mapper, CalculatorUnitOfWork calculatorUnitOfWork)
        {
            _mapper = mapper;
            _calculatorUnitOfWork = calculatorUnitOfWork;
        }

        public async Task<List<GetArticuloCalDto>> GetArticuloCals(string filter)
        {
            //var query = _calculatorUnitOfWork._articuloCalcRepository
            //    .GetAll(l => l.FlagActivo && l.IdEmpresa == 5, includeProperties: "ArticuloCategorium,ListaPrecioItems,RentabilidadComisions");

            var query = await _calculatorUnitOfWork._articuloCalcRepository.GetArticulosAll(filter);

            var response = _mapper.Map<List<GetArticuloCalDto>>(query);

            return response;
        }

      
    }
}
