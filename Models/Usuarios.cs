using System.ComponentModel.DataAnnotations;

namespace P01_2022PD651_2022VZ650_PARQUEO.Models
{
    public class Usuarios
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public enum rol
        {
            cliente,
            empleado
        }

    }
}
