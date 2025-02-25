using System.ComponentModel.DataAnnotations;

namespace P01_2022PD651_2022VZ650_PARQUEO.Models
{
    public class Reservas
    {
        [Key]
        public int id_reserva { get; set; }
        public int id_usuario { get; set; }
        public int id_espacio { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora_entrada { get; set; }
        public int cantidad_horas { get; set; }

    }
}
