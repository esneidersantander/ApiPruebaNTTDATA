using ApiPruebaNTTDATA.Controllers;
using ApiPruebaNTTDATA.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiPruebaNTTDATA.Tests.Tests
{
    [TestClass]
    public class ClientesControllerTests 
    {
        [TestMethod]
        public void Valida_Retorno_De_Cliente_Creado()
        {
            // Arrange
            ClientesController controller = new ClientesController();
            Cliente cliente = new Cliente()
            {
                Contrasena = "111111",
                Estado = true,
                Direccion = "13 junio y Equinoccial",
                Edad = 20,
                Genero = "M",
                Identificacion = "1314555267",
                Nombre = "test test",
                Telefono = "0999999999"
            };

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.CreateCliente(cliente) as CreatedNegotiatedContentResult<Cliente>;

            //// Assert
            Assert.IsInstanceOfType(actionResult.Content, typeof(Cliente));
        }

        [TestMethod]
        public void Verifica_NotFound_Con_Id_Inexistente_En_BD()
        {
            // Arrange
            int testClienteId = 777777;
            ClientesController controller = new ClientesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.GetCliente(testClienteId);
            var oddRes = actionResult.ExecuteAsync(CancellationToken.None).Result;

            //// Assert
            Assert.AreEqual(HttpStatusCode.NotFound, oddRes.StatusCode);

        }
    }
}
