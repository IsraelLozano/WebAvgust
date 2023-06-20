using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Articulos
{
    public class ArticuloRepository : GenericRepository<Articulo>, IArticuloRepository
    {
        private readonly dbContextAvgust _context;

        public ArticuloRepository(dbContextAvgust context) : base(context)
        {
            this._context = context;
        }

        public async Task<Articulo> GetArticuloFullById(int id)
        {
            try
            {
                var query = _context.Articulos.Where(p => p.IdArticulo == id)
                        .Select(p => new Articulo
                        {
                            IdArticulo = p.IdArticulo,
                            IdPais = p.IdPais,
                            NombreComercial = p.NombreComercial,
                            IdTitularRegistro = p.IdTitularRegistro,
                            NroRegistro = p.NroRegistro,
                            IdTipoProducto = p.IdTipoProducto,
                            IdFormulador = p.IdFormulador,
                            //IdGrupoQuimico = p.IdGrupoQuimico,
                            IdTipoFormulacion = p.IdTipoFormulacion,
                            IdTipoFormulacionNavigation = p.IdTipoFormulacion != null ? new TipoFormulacion
                            {
                                IdTipoFormulacion = p.IdTipoFormulacionNavigation.IdTipoFormulacion,
                                CodTipoFormulacion = p.IdTipoFormulacionNavigation.CodTipoFormulacion,
                                NomTipoFormulacion = p.IdTipoFormulacionNavigation.NomTipoFormulacion,
                            } : null,
                            //IdGrupoQuimicoNavigation = p.IdGrupoQuimico != null ? new GrupoQuimico
                            //{
                            //    IdGrupoQuimico = p.IdGrupoQuimicoNavigation.IdGrupoQuimico,
                            //    NomGrupoQuimico = p.IdGrupoQuimicoNavigation.NomGrupoQuimico
                            //} : null,
                            //IdFormuladorNavigation = p.IdFormulador != null ? new Formulador
                            //{
                            //    IdFormulador = p.IdFormuladorNavigation.IdFormulador,
                            //    NomFormulador = p.IdFormuladorNavigation.NomFormulador
                            //} : null,
                            IdPaisNavigation = p.IdPais != null ? new Pai
                            {
                                IdPais = p.IdPaisNavigation.IdPais,
                                NomPais = p.IdPaisNavigation.NomPais
                            } : null,
                            IdTipoProductoNavigation = p.IdTipoProducto != null ? new IdTipoProducto
                            {
                                IdTipoProducto1 = p.IdTipoProductoNavigation.IdTipoProducto1,
                                NomTipoProducto = p.IdTipoProductoNavigation.NomTipoProducto
                            } : null,
                            IdTitularRegistroNavigation = p.IdTitularRegistro != null ? new TitularRegistro
                            {
                                IdTitularRegistro = p.IdTitularRegistroNavigation.IdTitularRegistro,
                                NomTitularRegistro = p.IdTitularRegistroNavigation.NomTitularRegistro
                            } : null,

                            //Listas....
                            Composicions = p.Composicions.Select(c => new Composicion
                            {
                                IdArticulo = c.IdArticulo,
                                Iditem = c.Iditem,
                                IngredienteActivo = c.IngredienteActivo,
                                FormuladorMolecular = c.FormuladorMolecular,
                                idGrupoQuimico = c.idGrupoQuimico,
                                ContracionIA = c.ContracionIA,
                                IngredienteActivoNavigation = new IngredienteActivo
                                {
                                    IngredenteActivo = c.IngredienteActivoNavigation.IngredenteActivo,
                                    NomIngredienteActivo = c.IngredienteActivoNavigation.NomIngredienteActivo
                                },
                                GrupoQuimicoNavegation = new GrupoQuimico
                                {
                                    IdGrupoQuimico = c.GrupoQuimicoNavegation.IdGrupoQuimico,
                                    NomGrupoQuimico = c.GrupoQuimicoNavegation.NomGrupoQuimico
                                }
                            }).ToList(),
                            Documentos = p.Documentos.Select(d => new Documento
                            {
                                IdArticulo = d.IdArticulo,
                                IdItem = d.IdItem,
                                IdTipoDocumento = d.IdTipoDocumento,
                                Fecha = d.Fecha,
                                NomDocumento = d.NomDocumento,
                                IdTipoDocumentoNavigation = new TipoDocumento
                                {
                                    IdTipoDocumento = d.IdTipoDocumentoNavigation.IdTipoDocumento,
                                    Nombre = d.IdTipoDocumentoNavigation.Nombre
                                }
                            }).ToList() ?? new List<Documento>(),
                            Usos = p.Usos.Select(u => new Uso
                            {
                                IdArticulo = u.IdArticulo,
                                IdItem = u.IdItem,
                                NombreCientificoCultivo = u.NombreCientificoCultivo,
                                IdNomCientificoPlaga = u.IdNomCientificoPlaga,
                                Dosis = u.Dosis,
                                IdCultivo = u.IdCultivo,
                                IdCultivoNavigation = new Cultivo
                                {
                                    IdCultivo = u.IdCultivoNavigation.IdCultivo,
                                    NombreCultivo = u.IdCultivoNavigation.NombreCultivo
                                },
                                IdNomCientificoPlagaNavigation = new CientificoPlaga
                                {
                                    IdNomCientificoPlaga = u.IdNomCientificoPlagaNavigation.IdNomCientificoPlaga,
                                    NombreCientificoPlaga = u.IdNomCientificoPlagaNavigation.NombreCientificoPlaga
                                }

                            }).ToList(),
                            Caracteristicas = p.Caracteristicas.Select(c => new Caracteristica
                            {
                                IdArticulo = c.IdArticulo,
                                IdItem = c.IdItem,
                                //IdAplicacion = c.IdAplicacion,
                                IdClase = c.IdClase,
                                IdToxicologica = c.IdToxicologica,
                                IdClaseNavigation = new Clase
                                {
                                    IdClase = c.IdClaseNavigation.IdClase,
                                    Descripcion = c.IdClaseNavigation.Descripcion,
                                },
                                //IdAplicacionNavigation = new Aplicacion
                                //{
                                //    IdAplicacion = c.IdAplicacionNavigation.IdAplicacion,
                                //    Descripcion = c.IdAplicacionNavigation.Descripcion,
                                //},
                                IdToxicologicaNavigation = new Toxicologica
                                {
                                    IdToxicologica = c.IdToxicologicaNavigation.IdToxicologica,
                                    Descripcion = c.IdToxicologicaNavigation.Descripcion
                                }
                            }).ToList(),
                            ProductoFabricantes = p.ProductoFabricantes.Select(pf => new ProductoFabricante
                            {
                                IdFabricante = pf.IdFabricante,
                                IdArticulo = pf.IdArticulo,
                                IdFabricanteNavigation = new Fabricante
                                {
                                    IdFabricante = pf.IdFabricante,
                                    NombreFabricante = pf.IdFabricanteNavigation.NombreFabricante
                                }
                            }).ToList() ?? new List<ProductoFabricante>(),
                            ProductoFormuladors = p.ProductoFormuladors.Select(pFo => new ProductoFormulador
                            {
                                IdProducto = pFo.IdProducto,
                                IdFormualdor = pFo.IdFormualdor,
                                IdFormuladorNavigation = new Formulador
                                {
                                    IdFormulador = pFo.IdFormualdor,
                                    NomFormulador = pFo.IdFormuladorNavigation.NomFormulador
                                }
                            }).ToList() ?? new List<ProductoFormulador>()
                        });
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
