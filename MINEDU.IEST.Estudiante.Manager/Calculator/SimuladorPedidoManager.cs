using AutoMapper;
using IDCL.AVGUST.SIP.Entity.Calculator;
using IDCL.AVGUST.SIP.ManagerDto.Calculator.Simulador;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;

namespace IDCL.AVGUST.SIP.Manager.Calculator
{
    public class SimuladorPedidoManager : ISimuladorPedidoManager
    {
        private readonly IMapper _mapper;
        private readonly CalculatorUnitOfWork _calculatorUnitOfWork;

        public SimuladorPedidoManager(IMapper mapper, CalculatorUnitOfWork calculatorUnitOfWork)
        {
            _mapper = mapper;
            _calculatorUnitOfWork = calculatorUnitOfWork;
        }


        public async Task<List<GetPedidoDto>> getListPedidoAll()
        {
            var query = _calculatorUnitOfWork._simuladorPedidoRepository.GetAll(includeProperties: "SimuladorPedidoItems");

            var response = _mapper.Map<List<GetPedidoDto>>(query);

           
            return response;
        }
        public async Task<GetPedidoDto> GetPedidoById(int id)
        {
            var query = _calculatorUnitOfWork._simuladorPedidoRepository.GetAll(l => l.IdPedido == id, includeProperties: "SimuladorPedidoItems");

            var response = _mapper.Map<GetPedidoDto>(query.FirstOrDefault());

            return response;
        }





        public async Task<GetPedidoDto> addPedido(GetPedidoDto model)
        {
            var entidad = _mapper.Map<SimuladorPedido>(model);

            if (entidad.IdPedido == 0)
            {
                _calculatorUnitOfWork._simuladorPedidoRepository.Insert(entidad);
            }
            else
            {
                _calculatorUnitOfWork._simuladorPedidoRepository.Update(entidad);

            }

            _calculatorUnitOfWork.SaveAsync();

            return _mapper.Map<GetPedidoDto>(entidad);
        }

    }
}
