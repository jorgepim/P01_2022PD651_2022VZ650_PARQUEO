using System.ComponentModel.DataAnnotations;

namespace P01_2022PD651_2022VZ650_PARQUEO.Models
{
    public class Sucursales
    {
        [Key]
        public int id_sucursal { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public int id_usuario { get; set; }
        public int num_espacios { get; set; }
    }
}
