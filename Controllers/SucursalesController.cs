using Microsoft.AspNetCore.Mvc;
using P01_2022PD651_2022VZ650_PARQUEO.Models;

namespace P01_2022PD651_2022VZ650_PARQUEO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SucursalesController : Controller
    {
        private readonly parqueoContext _context;
        public SucursalesController(parqueoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var sucursales = (from s in _context.Sucursal
                              join u in _context.Usuario on s.id_usuario equals u.id_usuario
                              join p in _context.EspaciosParqueo on s.id_sucursal equals p.id_sucursal into espacios
                              select new
                              {
                                  s.id_sucursal,
                                  s.nombre,
                                  s.direccion,
                                  s.telefono,
                                  Administrador = u.nombre,
                                  Espacios = espacios.Count()
                              }).ToList();
            if (sucursales.Count == 0)
            {
                return NotFound();
            }
            return Ok(sucursales);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Sucursales sucursal)
        {
            Usuarios usuario = (from u in _context.Usuario
                                where u.id_usuario == sucursal.id_usuario
                                select u).FirstOrDefault();

            if (usuario.rol != "empleado")
            {
                return BadRequest("El usuario no es un empleado");
            }
            try
            {
                _context.Sucursal.Add(sucursal);
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
        public IActionResult Update(int id, [FromBody] Sucursales sucursal)
        {
            try
            {
                Sucursales sucursalActual = (from s in _context.Sucursal
                                             where s.id_sucursal == id
                                             select s).FirstOrDefault();
                if (sucursalActual == null)
                {
                    return NotFound();
                }
                sucursalActual.nombre = sucursal.nombre;
                sucursalActual.direccion = sucursal.direccion;
                sucursalActual.telefono = sucursal.telefono;
                sucursalActual.id_usuario = sucursal.id_usuario;
                sucursalActual.num_espacios = sucursal.num_espacios;
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
                Sucursales sucursal = (from s in _context.Sucursal
                                       where s.id_sucursal == id
                                       select s).FirstOrDefault();
                if (sucursal == null)
                {
                    return NotFound();
                }
                _context.Sucursal.Remove(sucursal);
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
