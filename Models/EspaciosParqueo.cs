using System.ComponentModel.DataAnnotations;

namespace P01_2022PD651_2022VZ650_PARQUEO.Models
{
    public class EspaciosParqueo
    {
        [Key]
        public int id_espacio { get; set; }
        public int id_sucursal { get; set; }
        public int numero { get; set; }
        public string ubicacion { get; set; }
        public Decimal costo_por_hora { get; set; }
        public string estado { get; set; }
    }
}
