using Microsoft.AspNetCore.Mvc;
using P01_2022PD651_2022VZ650_PARQUEO.Models;

namespace P01_2022PD651_2022VZ650_PARQUEO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly parqueoContext _context;
        public UsuariosController(parqueoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Usuarios> usuarios = _context.Usuario.ToList();
            if (usuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            Usuarios usuarioActual = (from u in _context.Usuario
                                      where u.correo == login.correo && u.contrasena == login.contrasena
                                      select u).FirstOrDefault();
            if (usuarioActual == null)
            {
                return BadRequest("El usuario no existe o usuario o contraseña esta incorrecto");
            }
            return Ok(new
            {
                message="Acceso concedido",
                usuario = usuarioActual
            });
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Usuarios usuario)
        {
            if (usuario.rol != "cliente" && usuario.rol != "empleado")
            {
                return BadRequest("El rol debe ser cliente o empleado");
            }
            try
            {
                _context.Usuario.Add(usuario);
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
        public IActionResult Update(int id, [FromBody] Usuarios usuario)
        {
            if (usuario.rol != "cliente" && usuario.rol != "empleado")
            {
                return BadRequest("El rol debe ser cliente o empleado");
            }
            try
            {
                Usuarios usuarioActual = (from u in _context.Usuario
                                          where u.id_usuario == id
                                          select u).FirstOrDefault();
                if (usuarioActual == null)
                {
                    return NotFound();
                }
                usuarioActual.nombre = usuario.nombre;
                usuarioActual.correo = usuario.correo;
                usuarioActual.contrasena = usuario.contrasena;
                usuarioActual.rol = usuario.rol;
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
                Usuarios usuario = (from u in _context.Usuario
                                    where u.id_usuario == id
                                    select u).FirstOrDefault();
                if (usuario == null)
                {
                    return NotFound();
                }
                _context.Usuario.Remove(usuario);
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
