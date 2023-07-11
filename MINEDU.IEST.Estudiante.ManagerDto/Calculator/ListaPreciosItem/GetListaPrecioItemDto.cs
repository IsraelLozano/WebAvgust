using IDCL.AVGUST.SIP.ManagerDto.Calculator.ListaPrecioItemDet;

namespace IDCL.AVGUST.SIP.ManagerDto.Calculator.ListaPreciosItem
{
    public class GetListaPrecioItemDto
    {
        public int IdListaPrecio { get; set; }
        public decimal PrecioBruto { get; set; }
        public bool tienePromo { get; set; }
        public List<GetListaPrecioItemDetDto> ListaPrecioItemDets { get; set; }
    }
}
