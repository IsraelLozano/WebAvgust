using AutoMapper;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using IDCL.AVGUST.SIP.ManagerDto.Maestros;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad;
using IDCL.AVGUST.SIP.ManagerDto.Seguridad.Add;
using IDCL.AVGUST.SIP.Repository.UnitOfWork;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.EmailSender;

namespace IDCL.AVGUST.SIP.Manager.Seguridad
{
    public class SeguridadManager : ISeguridadManager
    {
        private readonly SeguridadUnitOfWork _seguridadUnitOfWork;
        private readonly IMapper _mapper;
        private readonly MaestraUnitOfWork _maestraUnitOfWork;
        private readonly IEmailSender _emailSender;
        public SeguridadManager(SeguridadUnitOfWork seguridadUnitOfWork, IMapper mapper, MaestraUnitOfWork maestraUnitOfWork, IEmailSender emailSender)
        {
            this._seguridadUnitOfWork = seguridadUnitOfWork;
            this._mapper = mapper;
            this._maestraUnitOfWork = maestraUnitOfWork;
            _emailSender = emailSender;
        }

        public async Task<GetUsuarioDto> GetUsuarioAutenticar(string codigo, string clave, int idPais)
        {
            try
            {
                var pass = EncryptHelper.EncryptToByte(clave);
                var query = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.Credencial == codigo && p.Clave == pass, includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation").FirstOrDefault();

                if (query == null)
                    return new GetUsuarioDto();

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
                var query = _seguridadUnitOfWork._usuarioRepositoy.GetAll(includeProperties: "UsuarioPais,UsuarioPais.IdPaisNavigation", orderBy: p => p.OrderByDescending(l => l.IdUsuario));
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

        public async Task<AddOrEditUserDto> CreateOrUpdateUsuario(AddOrEditUserDto model)
        {
            try
            {
                var user = _mapper.Map<Usuario>(model);
                user.FechaRegistro = user.FechaModificacion = DateTime.Now;
                user.UsuarioRegistro = user.UsuarioModificacion = "SISTEMAS";

                if (user.IdUsuario == 0)
                {
                    var pass = EncryptHelper.EncryptToByte(model.TextoClave);
                    user.Clave = pass;
                    _seguridadUnitOfWork._usuarioRepositoy.Insert(user);

                    //Enviar correo...
                    var message = new Message(new string[] { model.Email } //, query.persona_institucion.FirstOrDefault().CORREO
                         , "Activacion de clave"
                         , new string[]
                         {
                                model.Nombres,
                                 model.Credencial,
                                model.TextoClave
                         }
                         , null);

                    await _emailSender.SendEmailRestauraClaveAsync(message);
                }
                else
                {
                    var userTemp = _seguridadUnitOfWork._usuarioRepositoy.GetById(model.IdUsuario);
                    userTemp.ApellidoPaterno = model.ApellidoPaterno;
                    userTemp.ApellidoMaterno = model.ApellidoMaterno;
                    userTemp.Nombres = model.Nombres;
                    userTemp.Email = model.Email;

                    _seguridadUnitOfWork._usuarioRepositoy.Update(userTemp);
                }

                await _seguridadUnitOfWork.SaveAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetUsuarioDto> GetForgotPassword(string email, string codigo)
        {
            try
            {
                var response = new GetUsuarioDto();
                var query = _seguridadUnitOfWork._usuarioRepositoy.GetAll(p => p.Credencial.ToUpper() == codigo.ToUpper() && p.Email.ToUpper() == email.ToUpper()).FirstOrDefault();

                if (query != null)
                {
                    response = _mapper.Map<GetUsuarioDto>(query);

                    var clave = Guid.NewGuid().ToString().Substring(0, 8);
                    await _seguridadUnitOfWork._usuarioRepositoy.UpdatePassword(query.Credencial, clave);

                    var message = new Message(new string[] { query.Email } //, query.persona_institucion.FirstOrDefault().CORREO
                  , "Restauración de clave"
                  , new string[]
                  {
                        response.Nombres,
                         codigo,
                        clave
                  }
                  , null);

                    await _emailSender.SendEmailRestauraClaveAsync(message);
                }

                return response;
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


        #region Usuario - Pais
        public async Task<List<GetPaisUsuarioDto>> GetListUsuarioPaisByIdUsuario(int idUsuario)
        {
            //var query = _seguridadUnitOfWork._usuarioPaisRepository.GetAll(p => p.IdUsuario == idUsuario);
            var query = await _maestraUnitOfWork._paisRepository.GetListUsuarioPaisByIdUsuario(idUsuario);
            var response = _mapper.Map<List<GetPaisUsuarioDto>>(query);
            response.ForEach(p =>
            {
                p.tieneUsuario = p.UsuarioPais.Count() > 0;
            });
            return response;
        }

        public async Task<bool> CreateOrUpdateUsuarioPais(List<AddOrEditUsuarioPaisDto> model)
        {
            try
            {
                var baseLine = _seguridadUnitOfWork._usuarioPaisRepository.GetAll(p => p.IdUsuario == model.FirstOrDefault().IdUsuario);

                var lista = _mapper.Map<List<UsuarioPai>>(model);

                foreach (var item in baseLine)
                {
                    _seguridadUnitOfWork._usuarioPaisRepository.Delete(item);
                }
                await _seguridadUnitOfWork.SaveAsync();

                foreach (var item in lista)
                {
                    _seguridadUnitOfWork._usuarioPaisRepository.Insert(item);
                }
                await _seguridadUnitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
