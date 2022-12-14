using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Maestros.Add;

namespace IDCL.AVGUST.SIP.Manager.Maestro
{
    public interface IMaestraManager
    {
        Task<bool> AnularAplicacion(int id);
        Task<bool> AnularCientificoPlaga(int id);
        Task<bool> AnularClase(int id);
        Task<bool> AnularCultivo(int id);
        Task<bool> AnularFormulador(int id);
        Task<bool> AnularGrupoQuimico(int id);
        Task<bool> AnularPais(int id);
        Task<bool> AnularTipoDocumento(int id);
        Task<bool> AnularTipoFormulacion(int id);
        Task<bool> AnularTipoIngredienteActivo(int id);
        Task<bool> AnularTipoProducto(int id);
        Task<bool> AnularTitularRegistro(int id);
        Task<bool> AnularToxicologia(int id);
        Task<GetAplicacionDto> CreateOrUpdateAplicacion(GetAplicacionDto model);
        Task<GetCientificoPlagaDto> CreateOrUpdateCientificoPlaga(GetCientificoPlagaDto model);
        Task<GetClaseDto> CreateOrUpdateClase(AddClaseDto model);
        Task<GetCultivoDto> CreateOrUpdateCultivo(GetCultivoDto model);
        Task<GetFormuladorDto> CreateOrUpdateFormulador(GetFormuladorDto model);
        Task<GetGrupoQuimicoDto> CreateOrUpdateGrupoQuimico(GetGrupoQuimicoDto model);
        Task<GetIdTipoProductoDto> CreateOrUpdateIdTipoProducto(GetIdTipoProductoDto model);
        Task<GetPaisDto> CreateOrUpdatePais(GetPaisDto model);
        Task<GetTipoDocumentoDto> CreateOrUpdateTipoDocumento(GetTipoDocumentoDto model);
        Task<GetTipoFormulacionDto> CreateOrUpdateTipoFormulacion(GetTipoFormulacionDto model);
        Task<GetTipoIngredienteActivoDto> CreateOrUpdateTipoIngredienteActivo(GetTipoIngredienteActivoDto model);
        Task<GetTitularRegistroDto> CreateOrUpdateTitularRegistro(GetTitularRegistroDto model);
        Task<GetToxicologicaDto> CreateOrUpdateToxicologica(GetToxicologicaDto model);
        Task<GetAplicacionDto> GetAplicacionById(int id);
        Task<GetCientificoPlagaDto> GetCientificoPlagaById(int id);
        Task<GetClaseDto> GetClaseById(int id);
        Task<GetCultivoDto> GetCultivoById(int id);
        Task<GetFormuladorDto> GetFormuladorById(int id);
        Task<GetGrupoQuimicoDto> GetGrupoQuimicoById(int id);
        Task<GetIdTipoProductoDto> GetIdTipoProductoById(int id);
        Task<List<GetAplicacionDto>> getListAplicacion();
        Task<List<GetCientificoPlagaDto>> getListCientificoPlaga();
        Task<List<GetClaseDto>> getListClase();
        Task<List<GetCultivoDto>> getListCultivo();
        Task<List<GetFormuladorDto>> getListFormulador();
        Task<List<GetGrupoQuimicoDto>> getListGrupoQuimico();
        Task<List<GetIdTipoProductoDto>> getListIdTipoProducto();
        Task<List<GetPaisDto>> getListPais();
        Task<List<GetTipoDocumentoDto>> getListTipoDocumento();
        Task<List<GetTipoFormulacionDto>> getListTipoFormulacion();
        Task<List<GetTipoIngredienteActivoDto>> getListTipoIngredienteActivo();
        Task<List<GetTitularRegistroDto>> getListTitularRegistro();
        Task<List<GetToxicologicaDto>> getListToxicologica();
        Task<GetPaisDto> GetPaisById(int id);
        Task<GetTipoDocumentoDto> GetTipoDocumentoById(int id);
        Task<GetTipoFormulacionDto> GetTipoFormulacionById(int id);
        Task<GetTipoIngredienteActivoDto> GetTipoIngredienteActivoById(int id);
        Task<GetTitularRegistroDto> GetTitularRegistroById(int id);
        Task<GetToxicologicaDto> GetToxicologicaById(int id);
    }
}