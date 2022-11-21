using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;

namespace IDCL.AVGUST.SIP.Manager.Seguridad
{
    public class SeguridadManager : ISeguridadManager
    {
        private readonly SeguridadUnitOfWork _seguridadUnitOfWork;
        private readonly IMapper _mapper;
        private readonly MaestraUnitOfWork _maestraUnitOfWork;

        public SeguridadManager(SeguridadUnitOfWork seguridadUnitOfWork, IMapper mapper, MaestraUnitOfWork maestraUnitOfWork)
        {
            this._seguridadUnitOfWork = seguridadUnitOfWork;
            this._mapper = mapper;
            this._maestraUnitOfWork = maestraUnitOfWork;
        }

        public async Task<GetUsuarioDto> GetUsuarioAutenticar(string codigo, string clave, int idPais)
        {
            try
            {
                var pass = EncryptHelper.EncryptToByte(clave);
                var query = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.Credencial == codigo && p.Clave == pass, includeProperties: "UsuarioPais").FirstOrDefault();
                var response = _mapper.Map<GetUsuarioDto>(query);

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region Gestion - Usuarios
        public async Task<List<GetUsuarioDto>> GetListUsuarios()
        {
            try
            {
                var query = _seguridadUnitOfWork._usuarioRepositoy.GetAll().OrderByDescending(p => p.IdUsuario);
                var response = _mapper.Map<List<GetUsuarioDto>>(query);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetUsuarioForEditDto> GetUsuarioById(int idUsuario)
        {
            try
            {
                var dtoMain = new GetUsuarioForEditDto();

                var query = _seguridadUnitOfWork._usuarioRepositoy.GetAll(includeProperties: "UsuarioPais");
                var usuario = _mapper.Map<GetUsuarioDto>(query);
                dtoMain.usuario = usuario;
                dtoMain.listPaises = _mapper.Map<List<GetPaisDto>>(_maestraUnitOfWork._paisRepository.GetAll());
                return dtoMain;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetUsuarioDto> CreateOrUpdateUsuario(GetUsuarioDto model)
        {
            try
            {
                var user = _mapper.Map<Usuario>(model);
                if (user.IdUsuario == 0)
                {
                    _seguridadUnitOfWork._usuarioRepositoy.Insert(user);
                }
                else
                {
                    _seguridadUnitOfWork._usuarioRepositoy.Update(user);
                }

                await _seguridadUnitOfWork.SaveAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public async Task<bool> DeleteUserById(int id)
        //{
        //    try
        //    {
        //        var query = _seguridadUnitOfWork._usuarioRepositoy.GetById(id);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        #endregion
    }
}
