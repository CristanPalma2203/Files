using Aplicacion.Commands;
using Aplicacion.Dtos;
using Aplicacion.Services.Comandos;
using Dominio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public ArchivoController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var archivo = (DescargarArchivoDto)commandBus.execute(new ConsultarArchivo { IdArchivo = id });
            return File(archivo.File, archivo.ContentType, archivo.FileName);
        }

        [HttpPost]
        public IResponse Post(IFormFile file)
        {
            return commandBus.execute(new CargarArchivo { File = file });
        }

        [HttpPost("registro", Name = "archivoRegistro")]
        public IResponse SubirArchivoRegistro(IFormFile file)
        {
            return commandBus.execute(new CargarArchivoRegistro { File = file });
        }
    }
}
