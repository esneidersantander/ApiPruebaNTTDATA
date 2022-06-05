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
    public class CuentasControllerTests
    {
        [TestMethod]
        public void Verifica_NotFound_Con_Id_Inexistente_En_BD()
        {
            // Arrange
            int testCuentaId = 77777;
            CuentasController controller = new CuentasController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.GetCuenta(testCuentaId);
            var oddRes = actionResult.ExecuteAsync(CancellationToken.None).Result;

            //// Assert
            Assert.AreEqual(HttpStatusCode.NotFound, oddRes.StatusCode);

        }

        [TestMethod]
        public void Valida_Id_Cliente_Exista_En_BD_Al_Crear_Cuenta()
        {
            // Arrange
            CuentasController controller = new CuentasController();
            Cuenta cuenta = new Cuenta()
            {
                    ClienteId= 777777,
                    NumeroCuenta= "496825",
                    TipoCuenta= "AHORRO",
                    SaldoInicial= 540,
                    Estado= true
            };

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.PostCuenta(cuenta);
            var oddRes = actionResult.ExecuteAsync(CancellationToken.None).Result;

            //// Assert
            Assert.AreEqual(HttpStatusCode.NotFound, oddRes.StatusCode);
        }

    }
}
