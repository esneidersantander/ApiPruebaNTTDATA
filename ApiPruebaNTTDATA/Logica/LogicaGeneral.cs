using ApiPruebaNTTDATA.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ApiPruebaNTTDATA.Logica
{
    public class LogicaGeneral : ApiController
    {
        private readonly MyDbContext _context;

        public LogicaGeneral()
        {
            _context = new MyDbContext();
        }

        public string MensajeError(ModelStateDictionary state)
        { 
            return state.Values.SelectMany(m => m.Errors)
                                                  .Select(e => !string.IsNullOrEmpty(e.ErrorMessage)
                                                            ? e.ErrorMessage
                                                            : e.Exception?.Message)
                                                  .FirstOrDefault().ToString().Split('.')[0];
        }

    }
}