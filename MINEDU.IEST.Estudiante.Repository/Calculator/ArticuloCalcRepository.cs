using IDCL.AVGUST.SIP.Contextos.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Entity.Calculator;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Calculator
{
    public class ArticuloCalcRepository : GenericRepository<ArticuloServ>, IArticuloCalcRepository
    {
        private readonly DbAvgustCalcContext _context;

        public ArticuloCalcRepository(DbAvgustCalcContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<ArticuloServ>> GetArticulosAll(string filter)
        {
            filter = filter ?? string.Empty;

            var query = await _context.ArticuloServs
                        .Where(l => l.IdEmpresa == 5
                        && l.IdTipoArticulo == 333001
                        && l.FlagActivo
                        && (l.NomArticulo.Contains(filter) || filter.Contains(l.NomArticulo))
                        && l.RentabilidadComisions.Any())

                        .Select(l => new ArticuloServ
                        {
                            IdEmpresa = l.IdEmpresa,
                            IdArticulo = l.IdArticulo,
                            CodArticulo = l.CodArticulo,
                            NomArticulo = l.NomArticulo,
                            NomArticuloEng = l.NomArticuloEng,
                            NomArticuloLargo = l.NomArticuloLargo,
                            NomCorto = l.NomCorto,
                            IndCodBarra = l.IndCodBarra,
                            CodBarra = l.CodBarra,
                            ArticuloCategorium = new ArticuloCategorium
                            {
                                NombreCategoria = l.ArticuloCategorium.NombreCategoria
                            },
                            ListaPrecioItems = l.ListaPrecioItems.Where(s => s.IdListaPrecio == 10).Select(p => new ListaPrecioItem
                            {
                                IdListaPrecio = p.IdListaPrecio,
                                PrecioVenta = p.PrecioVenta,
                                PrecioBruto = p.PrecioBruto,
                                ListaPrecioItemDets = p.ListaPrecioItemDets.Select(lt => new ListaPrecioItemDet
                                {
                                    Iditem = lt.Iditem,
                                    DesdeCantidad = lt.DesdeCantidad,
                                    PrecioBruto = lt.PrecioBruto,
                                }).OrderBy(o => o.Iditem).ToList()
                            }).ToList(),
                            RentabilidadComisions = l.RentabilidadComisions.Where(z => z.IdArticulo == l.IdArticulo).Select(r => new RentabilidadComision
                            {
                                Porcentaje = r.Porcentaje,
                                CategoriaRes = r.CategoriaRes,
                                CostoUnit = r.CostoUnit
                            }).ToList(),
                        })
                        .Take(500)
                        .ToListAsync();
            return query;
        }

    }
}
