using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VetAdmin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public string GET()
        {
            return "hola que haces?";
        }
    }
}
