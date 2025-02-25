using Microsoft.AspNetCore.Mvc;
using P01_2022PD651_2022VZ650_PARQUEO.Models;

namespace P01_2022PD651_2022VZ650_PARQUEO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EspaciosParqueoController : Controller
    {
        private readonly parqueoContext _context;
        public EspaciosParqueoController(parqueoContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var espacios = (from e in _context.EspaciosParqueo
                            join s in _context.Sucursal on e.id_sucursal equals s.id_sucursal
                            select new
                            {
                                e.id_espacio,
                                e.numero,
                                e.ubicacion,
                                e.costo_por_hora,
                                e.estado,
                                Sucursal = s.nombre
                            }).ToList();

            if (espacios.Count == 0)
            {
                return NotFound();
            }
            return Ok(espacios);
        }

        [HttpGet]
        [Route("GetAvailable")]
        public IActionResult GetAvailable()
        {
            var espacios = (from e in _context.EspaciosParqueo
                            join s in _context.Sucursal on e.id_sucursal equals s.id_sucursal
                            where e.estado == "disponible"
                            select new
                            {
                                e.id_espacio,
                                e.numero,
                                e.ubicacion,
                                e.costo_por_hora,
                                e.estado,
                                Sucursal = s.nombre
                            }).ToList();
            if (espacios.Count == 0)
            {
                return NotFound();
            }
            return Ok(espacios);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] EspaciosParqueo espacio)
        {
            if (espacio.estado != "disponible" && espacio.estado != "ocupado")
            {
                return BadRequest("El estado debe ser disponible o ocupado");
            }
            try
            {
                _context.EspaciosParqueo.Add(espacio);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult Update(int id, [FromBody] EspaciosParqueo espacio)
        {
            if (espacio.estado != "disponible" && espacio.estado != "ocupado")
            {
                return BadRequest("El estado debe ser disponible o ocupado");
            }
            try
            {
                EspaciosParqueo espacioActual = (from e in _context.EspaciosParqueo
                                                 where e.id_espacio == id
                                                 select e).FirstOrDefault();
                if (espacioActual == null)
                {
                    return NotFound();
                }
                espacioActual.id_sucursal = espacio.id_sucursal;
                espacioActual.numero = espacio.numero;
                espacioActual.ubicacion = espacio.ubicacion;
                espacioActual.costo_por_hora = espacio.costo_por_hora;
                espacioActual.estado = espacio.estado;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EspaciosParqueo espacio = (from e in _context.EspaciosParqueo
                                           where e.id_espacio == id
                                           select e).FirstOrDefault();
                if (espacio == null)
                {
                    return NotFound();
                }
                _context.EspaciosParqueo.Remove(espacio);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
