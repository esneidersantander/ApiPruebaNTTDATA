using ApiPruebaNTTDATA.Controllers;
using ApiPruebaNTTDATA.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace ApiPruebaNTTDATA.Tests.Tests
{
    [TestClass]
    public class MovimientosControllerTests
    {
        //ID DE CUENTA QUE EXISTE
        private readonly int CuentaId = 1;
        [TestMethod]
        public void Valida_Movimiento_Mayor_A_1000_Y_Retorna_BadRequest()
        {
            // Arrange
            int testValor = 1001;
            MovimientosController controller = new MovimientosController();
            Movimiento mov = new Movimiento()
            {
                CuentaId= CuentaId,
                TipoMovimiento= "RETIRO",
                Valor=testValor
            };

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.CreateMovimiento(mov);
            var oddRes = actionResult.ExecuteAsync(CancellationToken.None).Result;

            //// Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, oddRes.StatusCode);
        }
        [TestMethod]
        public void Valida_Retorno_NotFound_Cuando_La_Cuenta_No_existe()
        {
            MovimientosController controller = new MovimientosController();
            Movimiento mov = new Movimiento()
            {
                CuentaId = 777777,
                TipoMovimiento = "DEPOSITO",
                Valor = 999
            };

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.CreateMovimiento(mov);
            var oddRes = actionResult.ExecuteAsync(CancellationToken.None).Result;

            //// Assert
            Assert.AreEqual(HttpStatusCode.NotFound, oddRes.StatusCode);
        }
    }
}
