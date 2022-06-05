using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using ApiPruebaNTTDATA.Dtos;
using ApiPruebaNTTDATA.Logica;
using ApiPruebaNTTDATA.Models;
using AutoMapper;

namespace ApiPruebaNTTDATA.Controllers
{
    public class CuentasController : ApiController
    {
        private readonly MyDbContext _context;
        private LogicaGeneral _logica;


        public CuentasController()
        {
            _logica = new LogicaGeneral();
            _context = new MyDbContext();
        }

        [HttpGet]
        public IEnumerable<Cuenta> GetCuentas()
        {
            return _context.Cuentas.Include(c => c.Cliente).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetCuenta(int id)
        {
            var cuenta = _context.Cuentas.Include(c=>c.Cliente).FirstOrDefault(d=>d.Id == id);

            if (cuenta == null)
            {
                return Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró el elemento indicado." });
            }

            return Ok(cuenta);
        }

        [HttpPut]
        public IHttpActionResult PutCuenta(int id, CuentaDto cuentaDto)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = _logica.MensajeError(ModelState) });
            }
            Cuenta cuentaInDb = _context.Cuentas.SingleOrDefault(c => c.Id == id);
            cuentaDto.ClienteId = cuentaInDb.ClienteId;
            Mapper.Map(cuentaDto, cuentaInDb);
            _context.SaveChanges();
            return Ok(cuentaInDb);
        }

        [HttpPost]
        public IHttpActionResult PostCuenta(Cuenta cuenta)
        {
            Cliente cliente = _context.Clientes.SingleOrDefault(c => c.Id == cuenta.ClienteId);
            if (cliente == null)
            {
                return Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró un cliente con el id de cliente indicado." });
            }
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = _logica.MensajeError(ModelState) });
            }
            _context.Cuentas.Add(cuenta);
            _context.SaveChanges();

            return Created(Request.RequestUri + "/" +  cuenta.Id, cuenta);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCuenta(int id)
        {
            var cuentaInDb = _context.Cuentas.SingleOrDefault(c => c.Id == id);
            if (cuentaInDb == null)
            {
                return Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró el elemento indicado." });
            }

            _context.Cuentas.Remove(cuentaInDb);
            _context.SaveChanges();

            return Ok(new Respuesta() { Mensaje = "Se eliminó correctamente" });
        }
    }
}
