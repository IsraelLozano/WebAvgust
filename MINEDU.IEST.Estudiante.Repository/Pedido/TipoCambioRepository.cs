﻿using IDCL.AVGUST.SIP.Contexto.DataPedido;
using IDCL.AVGUST.SIP.Entity.Pedido;
using MINEDU.IEST.Estudiante.Repository.Base;

namespace IDCL.AVGUST.SIP.Repository.Pedido
{
    public class TipoCambioRepository : GenericRepository<TipoCambio>, ITipoCambioRepository
    {
        public TipoCambioRepository(DbPedidoContext context) : base(context)
        {
        }
    }
}
