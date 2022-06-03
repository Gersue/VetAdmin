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

        Response validarCliente(Cliente cliente)
        {
            if (cliente.Noidentificacion == null || cliente.Noidentificacion.Length > 25 || cliente.Noidentificacion.Trim().Length == 0)
            {
                return new Response { Success = false, Message = "No_Identificacion es un dato requerido y no debe acceder los 25 caracteres" };
            }

            if (cliente.Apellido == null || cliente.Apellido.Length > 50)
            {
                return new Response { Success = false, Message = "El apellido es un campo requerido y no debe acceder los 50 caracteres" };
            }

            if (cliente.Correo.Length > 50)
            {
                return new Response { Success = false, Message = "El correo mo debe exceder los 50 caracteres" };
            }

            if (cliente.Direccion == null || cliente.Direccion.Length > 25)
            {
                return new Response { Success = false, Message = "La direccion es un dato requerido y no debe acceder los 25 caracteres" };
            }

            if (cliente.Nombre == null || cliente.Nombre.Length > 50)
            {
                return new Response { Success = false, Message = "Es dato requerido y nodebe acceder los 50 caracteres" };
            }

            if (cliente.Telefono == null || cliente.Telefono.Length > 8)
            {
                return new Response { Success = false, Message = "Es un dato requerido y no debe acceder los 8 caracteres" };
            }

            return new Response { Success = true };
        }

        [HttpPost]
        public Response Post(Cliente cliente)
        {
            Response response = validarCliente(cliente);

            if (!response.Success)
            {
                return response;
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

        [HttpGet("{id}")]
        public Response Get(int id)
        {
            var cliente = db.Clientes.Find(id);  //Expresion lambda
            if(cliente == null)
            {
                return new Response { Success = false, Message = "Registro no encontrado" };
            }
            else
            {
                return new Response { Success =true, Data = cliente };
            }
        }

        [HttpPut]
        public Response PUT(Cliente cliente)
        {

            if(cliente.Idcliente == null)
            {
                return new Response { Success = false, Message = "El id del cliente es un dato requerido" };
            }

            Response response = validarCliente(cliente);

            if (!response.Success)
            {
                return response;
            }

            Cliente clienteBD = db.Clientes.Find(cliente.Idcliente);

            if(clienteBD == null)
            {
                return new Response { Success = false, Message = "Registro no encontrado" };
            }
            clienteBD.Nombre = cliente.Nombre;
            clienteBD.Noidentificacion = cliente.Noidentificacion;
            clienteBD.Apellido = cliente.Apellido;
            clienteBD.Direccion = cliente.Direccion;
            clienteBD.Correo = cliente.Correo;
            clienteBD.Telefono = cliente.Telefono;

            db.SaveChanges();

            return new Response { Success = true, Data = clienteBD, Message="Datos Guardados"};

        }

        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            Cliente cliente = db.Clientes.Find(id);

            if(cliente == null)
            {
                return new Response { Success = false, Message = "Registro no encontrado" };
            }

            db.Clientes.Remove(cliente);
            db.SaveChanges(true);

            return new Response { Success = true, Message = "Registro Eliminado" };
        }

    }
}
