using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.Entity.Pedido;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Pedido;
using IDCL.AVGUST.SIP.ManagerDto.StoreProcedure;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;

namespace IDCL.AVGUST.SIP.Manager.Pedido
{
    public class PedidoManager : IPedidoManager
    {
        private readonly IMapper _mapper;
        private readonly PedidoUnitOfWork _pedidoUnitOfWork;

        public PedidoManager(IMapper mapper, PedidoUnitOfWork pedidoUnitOfWork)
        {
            this._mapper = mapper;
            this._pedidoUnitOfWork = pedidoUnitOfWork;
        }

        public async Task<List<GetCostoArticuloDto>> getListArticulos(int idArticulo, string codigo, string fechaStock)
        {
            if (codigo == null)
            {
                codigo = "%";
            }

            fechaStock = fechaStock ?? DateTime.Now.ToString("yyyyMMdd");

            var query = await _pedidoUnitOfWork._pedidoRepository.ListarCostoArticulo(idArticulo, codigo, fechaStock);

            return _mapper.Map<List<GetCostoArticuloDto>>(query);
        }

        public async Task<AddPedidoDto> AddPedido(AddPedidoDto model)
        {
            var pedido = _mapper.Map<PedidoCab>(model);
            var resp = new AddPedidoDto();

            try
            {
                var cliente = _pedidoUnitOfWork._personaRepository.GetAll(l => l.Ruc == model.NroRuc).FirstOrDefault();

                if (cliente != null)
                {
                    pedido.IdFacturar = cliente.IdPersona;
                }
                else
                {
                    resp.EsError = true;
                    resp.MensajeError = "El ruc ingresado no existe o no es valido";
                    return resp;
                }
                //Obtener Codigo Pedido 
                var nroPedido = await _pedidoUnitOfWork._pedidoRepository.ObtenerNroPedido(5, 1, "P");

                //Adicionales
                pedido.IdEmpresa = 5;
                pedido.IdLocal = 1;
                pedido.CodPedidoCad = nroPedido;
                pedido.UsuarioModificacion = pedido.UsuarioRegistro;
                pedido.FechaModificacion = pedido.FechaRegistro;
                pedido.IndCotPed = "P";
                pedido.IdTipCondicion = 1;
                pedido.Estado = "1";
                pedido.NroGuia = pedido.NroFactura = pedido.NroGuiaGen = pedido.NroFacturaGen = string.Empty;
                pedido.FecFactura = null;
                pedido.TipoGeneracion = "N";
                var tc = _pedidoUnitOfWork._tipoCambioRepository
                                            .GetAll(l => l.IdMoneda == "02" && l.FecCambio.Date == pedido.Fecha.Date)
                                            .FirstOrDefault();
                if (tc != null)
                    pedido.TipCambio = tc.ValVenta;

                pedido.PedidoDets.ForEach(l =>
                {
                    l.IdEmpresa = pedido.IdEmpresa;
                    l.IdLocal = pedido.IdLocal;
                    l.CantidadUnit = 0;
                    l.CantidadFinal = l.Cantidad;
                    l.UsuarioRegistro = l.UsuarioModificacion = pedido.UsuarioRegistro;
                    l.FechaRegistro = l.FechaModificacion = pedido.FechaRegistro;
                });

                _pedidoUnitOfWork._pedidoRepository.Insert(pedido);
                await _pedidoUnitOfWork.SaveAsync();

                var result = _mapper.Map<AddPedidoDto>(pedido);
                result.IdFacturar = pedido.IdPedido;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
