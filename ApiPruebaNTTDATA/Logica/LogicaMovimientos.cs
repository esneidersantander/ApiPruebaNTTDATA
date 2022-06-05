using ApiPruebaNTTDATA.Dtos;
using ApiPruebaNTTDATA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ApiPruebaNTTDATA.Logica
{
    public class LogicaMovimientos 
    {
        private readonly MyDbContext _context;

        public LogicaMovimientos()
        {
            _context = new MyDbContext();
        }
        public IEnumerable<ReporteMovimientos> ConsultarMovimientosFechaUsuario(InputReporteMovimientos input) 
        {

            IEnumerable<Movimiento> movimientos = _context.Movimientos.Where(p => p.Fecha >= input.FechaInicio && p.Fecha < input.FechaFin && p.Cuenta.ClienteId == input.ClienteId)
                                                .Include(c => c.Cuenta)
                                                .Include(e => e.Cuenta.Cliente)
                                                .ToList();
            List<ReporteMovimientos> movimientosReporte = new List<ReporteMovimientos>();
            foreach (var mov in movimientos)
            {
                ReporteMovimientos aux = new ReporteMovimientos();
                aux.Fecha = mov.Fecha;
                aux.Cliente = mov.Cuenta.Cliente.Nombre;
                aux.NumeroCuenta = mov.Cuenta.NumeroCuenta;
                aux.TipoCuenta = mov.Cuenta.TipoCuenta;
                aux.SaldoInicial = mov.TipoMovimiento == "RETIRO" ? mov.Saldo + mov.Valor : mov.Saldo - mov.Valor;
                aux.EstadoCuenta = mov.Cuenta.Estado;
                aux.TipoMovimiento = mov.TipoMovimiento;
                aux.SaldoDisponible = mov.Saldo;
                movimientosReporte.Add(aux);
            }
            return movimientosReporte;
        }

        public IEnumerable<Movimiento> ConsultaMovimientosFechaCuenta(int CuentaId)
        {
            DateTime hoy = DateTime.Today;
            DateTime manana = hoy.AddDays(1);

            return _context.Movimientos.Where(p => p.Fecha >= hoy && p.Fecha < manana && p.CuentaId == CuentaId)
                                        .ToList();

        }
    }
}