using AutoMapper;
using IDCL.AVGUST.SIP.ManagerDto.Calculator;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.ArticuloCalc;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using System.Collections.Generic;

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

        public async Task<GetArticuloAllDto> GetArticuloCals(string filter)
        {
            //var query = _calculatorUnitOfWork._articuloCalcRepository
            //    .GetAll(l => l.FlagActivo && l.IdEmpresa == 5, includeProperties: "ArticuloCategorium,ListaPrecioItems,RentabilidadComisions");

            var response = new GetArticuloAllDto();

            var query = await _calculatorUnitOfWork._articuloCalcRepository.GetArticulosAll(filter);

            response.articulos = _mapper.Map<List<GetArticuloCalDto>>(query);
            response.tasaComisions = _mapper.Map<List<GetTasaComisionDto>>(_calculatorUnitOfWork._tasaComisionRepository.GetAll());
            return response;
        }


    }
}
