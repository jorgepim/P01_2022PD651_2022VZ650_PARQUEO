using Microsoft.AspNetCore.Mvc;
using P01_2022PD651_2022VZ650_PARQUEO.Models;

namespace P01_2022PD651_2022VZ650_PARQUEO.Controllers
{
    [Route("Usuarios/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
