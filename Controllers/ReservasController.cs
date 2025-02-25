using Microsoft.AspNetCore.Mvc;
using P01_2022PD651_2022VZ650_PARQUEO.Models;

namespace P01_2022PD651_2022VZ650_PARQUEO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservasController : Controller
    {
        private readonly parqueoContext _context;
        public ReservasController(parqueoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var reservas = (from r in _context.Reserva
                            join u in _context.Usuario on r.id_usuario equals u.id_usuario
                            join e in _context.EspaciosParqueo on r.id_espacio equals e.id_espacio
                            join s in _context.Sucursal on e.id_sucursal equals s.id_sucursal
                            select new
                            {
                                r.id_reserva,
                                Usuario = u.nombre,
                                Espacio = e.numero,
                                Ubicación = e.ubicacion,
                                Sucursal = s.nombre,
                                r.fecha,
                                r.hora_entrada,
                                r.cantidad_horas
                            }).ToList();

            if (reservas.Count == 0)
            {
                return NotFound();
            }
            return Ok(reservas);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Reservas reserva)
        {
            try
            {
                _context.Reserva.Add(reserva);
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
        public IActionResult Update(int id, [FromBody] Reservas reserva)
        {
            try
            {
                Reservas reservaActual = (from r in _context.Reserva
                                          where r.id_reserva == id
                                          select r).FirstOrDefault();
                if (reservaActual == null)
                {
                    return NotFound();
                }
                reservaActual.id_usuario = reserva.id_usuario;
                reservaActual.id_espacio = reserva.id_espacio;
                reservaActual.fecha = reserva.fecha;
                reservaActual.hora_entrada = reserva.hora_entrada;
                reservaActual.cantidad_horas = reserva.cantidad_horas;
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
                Reservas reserva = (from r in _context.Reserva
                                    where r.id_reserva == id
                                    select r).FirstOrDefault();
                if (reserva == null)
                {
                    return NotFound();
                }
                _context.Reserva.Remove(reserva);
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
