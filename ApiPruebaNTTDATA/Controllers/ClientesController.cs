using System.Collections.Generic;
using System.Linq;
using ApiPruebaNTTDATA.Models;
using System.Web.Http;
using System.Net;
using ApiPruebaNTTDATA.Logica;
using ApiPruebaNTTDATA.Dtos;
using AutoMapper;

namespace ApiPruebaNTTDATA.Controllers
{
    public class ClientesController : ApiController
    {
        private readonly MyDbContext _context;
        private LogicaGeneral _logica;


        public ClientesController()
        {
            _logica = new LogicaGeneral();
            _context = new MyDbContext();
        }

        [HttpGet]
        public IEnumerable<Cliente> GetClientes()
        {
            return _context.Clientes.ToList();
        }

        [HttpGet]
        public IHttpActionResult GetCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(d => d.Id == id);

            if (cliente == null)
            {
                return Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró el elemento indicado." });
            }

            return Ok(cliente);
        }

        [HttpPut]
        public IHttpActionResult PutCliente(int id, ClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = _logica.MensajeError(ModelState) });
            }
            var clienteInDb = _context.Clientes.SingleOrDefault(c => c.Id == id);
            if (clienteInDb == null)
            {
                Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró el elemento indicado." });
            }
            Mapper.Map(clienteDto, clienteInDb);
            _context.SaveChanges();
            return Ok(clienteInDb);
        }

        [HttpPost]
        public IHttpActionResult CreateCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = _logica.MensajeError(ModelState) });
            }
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Created("/api/clientes/", cliente);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCliente(int id)
        {
            var clienteInDb = _context.Clientes.SingleOrDefault(c => c.Id == id);
            if (clienteInDb == null)
            {
                return Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró el elemento indicado." });
            }

            _context.Clientes.Remove(clienteInDb);
            _context.SaveChanges();
            return Ok(new Respuesta() { Mensaje = "Se eliminó correctamente" });
        }
    }
}
