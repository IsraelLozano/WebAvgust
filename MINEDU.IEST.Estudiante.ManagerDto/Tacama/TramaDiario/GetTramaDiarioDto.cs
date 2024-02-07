using DataAnnotationsExtensions;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using System.ComponentModel.DataAnnotations;

namespace IDCL.AVGUST.SIP.ManagerDto.Tacama.TramaDiario
{
    public class GetTramaDiarioDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Id de Persona es obligatorio")]
        [Min(min: 1, ErrorMessage = "El Id Persona que ingreso no es valido")]
        public int IdPersona { get; set; }
        public DateTime FechaHora { get; set; }
        [Required(ErrorMessage = "Longitud es obligatorio")]
        public decimal Longitud { get; set; }
        [Required(ErrorMessage = "Latitud es obligatorio")]
        public decimal Latitud { get; set; }
    }
}
