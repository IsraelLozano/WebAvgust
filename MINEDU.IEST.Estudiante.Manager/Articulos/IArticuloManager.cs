using IDCL.AVGUST.SIP.ManagerDto.Articulos;
using IDCL.AVGUST.SIP.ManagerDto.Articulos.Add;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;

namespace IDCL.AVGUST.SIP.Manager.Articulos
{
    public interface IArticuloManager
    {
        Task<AddOrEditArticuloDto> CreateOrUpdateArticulo(AddOrEditArticuloDto model);
        Task<AddOrEditCaracteristicaDto> CreateOrUpdateCaracteristica(AddOrEditCaracteristicaDto model);
        Task<AddOrEditComposicionDto> CreateOrUpdateComposicion(AddOrEditComposicionDto model);
        Task<AddOrEditDocumentoDto> CreateOrUpdateDocumento(AddOrEditDocumentoDto model);
        Task<bool> CreateOrUpdateProductoFabricante(List<AddOrEditProductoFabricanteDto> model);
        Task<bool> CreateOrUpdateProductoFormulador(List<AddOrEditProductoFormuladorDto> model);
        Task<AddOrEditUsoDto> CreateOrUpdateUso(AddOrEditUsoDto model);
        Task<bool> DeleteArticuloById(int id);
        Task<bool> DeleteCaracteristicaByItem(int idArticulo, int item);
        Task<bool> DeleteComposicionByItem(int idArticulo, int item);
        Task<bool> DeleteDocumentoByItem(int idArticulo, int item);
        Task<bool> DeleteProductoFabricanteById(int IdArticulo, int IdFabricante);
        Task<bool> DeleteProductoFormuladorById(int IdArticulo, int IdFormulador);
        Task<bool> DeleteUsoByItem(int idArticulo, int item);
        Task<GetArticuloForEditDto> GetArticuloById(int id);
        Task<GetPdfDto> GetArticuloDocumentoPdf(int idArticulo, int idItem);
        Task<string> GetEtiquetaDocumento(int idArticulo);
        Task<List<GetArticuloDto>> GetListArticulos(int IdUsuario, int tipoFiltro, string filtro, int idIngredienteActivo);
        Task<List<GetCaracteristicaDto>> GetListCaracteristicaByIdArticulo(int idArticulo);
        Task<List<GetComposicionDto>> GetListComposicionByIdArticulo(int idArticulo);
        Task<List<GetDocumentoDto>> GetListDocumentoByIdArticulo(int idArticulo);
        Task<List<GetUsoDto>> GetListUsoByIdArticulo(int idArticulo);
    }
}