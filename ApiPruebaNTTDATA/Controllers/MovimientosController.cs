using ApiPruebaNTTDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Data.Entity;
using ApiPruebaNTTDATA.Dtos;
using ApiPruebaNTTDATA.Logica;

namespace ApiPruebaNTTDATA.Controllers
{
    public class MovimientosController : ApiController
    {
        private readonly MyDbContext _context;
        private LogicaGeneral _logica;
        private LogicaMovimientos _logicaMov;

        public MovimientosController()
        {
            _logicaMov = new LogicaMovimientos();
            _logica = new LogicaGeneral();
            _context = new MyDbContext();
        }
        [HttpGet]
        public IEnumerable<Movimiento> GetMovimiento()
        {
            return _context.Movimientos.Include(c => c.Cuenta).Include(e=>e.Cuenta.Cliente).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetMovimiento(int id)
        {
            var movimiento = _context.Movimientos.Find(id);

            if (movimiento == null)
            {
                return Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró el elemento indicado." });
            }

            return Ok(movimiento);
        }

        [HttpPost]
        public IHttpActionResult CreateMovimiento(Movimiento movimiento)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = _logica.MensajeError(ModelState) });
            }
            Cuenta cuenta = _context.Cuentas.SingleOrDefault(c => c.Id == movimiento.CuentaId);
            if (cuenta == null)
            {   
                return Content(HttpStatusCode.NotFound, new Respuesta() { Mensaje = "No se encontró una cuenta con el id de cuenta indicado." });
            }

            if (movimiento.TipoMovimiento == "RETIRO" && (cuenta.SaldoInicial - movimiento.Valor) < 0)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = "Saldo no disponible." });
            }
            IEnumerable<Movimiento> movimientos = _logicaMov.ConsultaMovimientosFechaCuenta(movimiento.CuentaId);
            movimiento.Fecha = DateTime.Now;
            if (movimiento.Valor > 1000)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = "Cupo diario Excedido." });
            }

            if (movimientos!=null)
            {
                double diario = 0;
                foreach (var mov in movimientos)
                {
                    if (mov.TipoMovimiento == "RETIRO")
                    {
                        diario += mov.Valor;
                        if (diario > 1000 || (diario + movimiento.Valor) > 1000 )
                        {
                            return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = "Cupo diario Excedido." });
                        }
                    }
                }
            }

            if (movimiento.TipoMovimiento == "RETIRO")
            {
                movimiento.Saldo = cuenta.SaldoInicial - movimiento.Valor;
                cuenta.SaldoInicial = cuenta.SaldoInicial - movimiento.Valor;
            }
            else
            {
                movimiento.Saldo = cuenta.SaldoInicial + movimiento.Valor;
                cuenta.SaldoInicial = cuenta.SaldoInicial + movimiento.Valor;
            }

            _context.Movimientos.Add(movimiento);
            _context.SaveChanges();

            return Created("/api/movimientos/", movimiento);
        }

        [Route("api/movimientos/reporte")]
        [HttpGet]
        public IHttpActionResult GetMovimientoReporte(InputReporteMovimientos input)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Respuesta() { Mensaje = _logica.MensajeError(ModelState) });
            }
            return Ok(_logicaMov.ConsultarMovimientosFechaUsuario(input));
        }
    }
}
