using Aplicacion.Common;
using Microsoft.AspNetCore.Http;

namespace Aplicacion.Commands
{
    public class CargarArchivo : IAppMessage
    {
        public IFormFile File { get; set; }
    }
}
