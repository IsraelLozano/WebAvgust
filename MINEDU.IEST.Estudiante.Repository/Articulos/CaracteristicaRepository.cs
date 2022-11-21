using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using MINEDU.IEST.Estudiante.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCL.AVGUST.SIP.Repository.Articulos
{
    public class CaracteristicaRepository : GenericRepository<Caracteristica>, ICaracteristicaRepository
    {
        public CaracteristicaRepository(dbContextAvgust context) : base(context)
        {
        }
    }

    
}
