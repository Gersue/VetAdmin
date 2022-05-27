using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAdmin.Context;
using System.Linq;
using VetAdmin.Models;  //Se importa para el Response del metodo GET
using System;

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
        public Response Post(Cliente cliente)
        {
            if(cliente.Noidentificacion == null)
            {
                return new Response { Success = false, Message = "No_Identificacion es un dato requerido" };
            }

            try {
                db.Clientes.Add(cliente);

                db.SaveChanges();

                return new Response { Success = true, Data = cliente };
            }
            catch(System.Exception ex) {
                Console.WriteLine(DateTime.Now + " - " +ex.Message + "Inner Details: "+ ex.InnerException.Message);
                return new Response { Success = false, Message = "Se ha producido un error" };
            }
        }
    }
}
