using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAdmin.Context;
using System.Linq;
using VetAdmin.Models;  //Se importa para el Response del metodo GET

namespace VetAdmin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        VetAdminDBContext db = new VetAdminDBContext();

        [HttpGet]
        public Response GET()
        {
            var data = (from Cliente in db.Clientes select Cliente).ToList();

            return new Response { Success = true, Data = data };
        }

        [HttpPost]
        public Response POST(Cliente cliente)
        {
            db.Clientes.Add(cliente);

            db.SaveChanges();

            return new Response { Success = true, Data = cliente };
        }
    }
}
