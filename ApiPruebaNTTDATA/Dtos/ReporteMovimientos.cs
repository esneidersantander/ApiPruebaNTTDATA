using System;

namespace ApiPruebaNTTDATA.Dtos
{
    public class ReporteMovimientos
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public double SaldoInicial { get; set; }
        public bool EstadoCuenta { get; set; }
        public string TipoMovimiento { get; set; }
        public double SaldoDisponible { get; set; }

    }
}