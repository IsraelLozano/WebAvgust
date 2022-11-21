using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;

namespace IDCL.AVGUST.SIP.Manager.Articulos
{
    public interface IArticuloManager
    {
        Task<AddOrEditArticuloDto> CreateOrUpdateArticulo(AddOrEditArticuloDto model);
        Task<AddOrEditCaracteristicaDto> CreateOrUpdateCaracteristica(AddOrEditCaracteristicaDto model);
        Task<AddOrEditComposicionDto> CreateOrUpdateComposicion(AddOrEditComposicionDto model);
        Task<AddOrEditDocumentoDto> CreateOrUpdateDocumento(AddOrEditDocumentoDto model);
        Task<AddOrEditUsoDto> CreateOrUpdateUso(AddOrEditUsoDto model);
        Task<GetArticuloForEditDto> GetArticuloById(int id);
        Task<List<GetArticuloDto>> GetListArticulos();
        Task<List<GetCaracteristicaDto>> GetListCaracteristicaByIdArticulo(int idArticulo);
        Task<List<GetComposicionDto>> GetListComposicionByIdArticulo(int idArticulo);
    }
}