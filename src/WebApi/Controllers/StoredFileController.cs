using Application.Commands;
using Application.Dtos;
using Application.Services.Comandos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoredFileController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public StoredFileController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var archivo = (DownloadFileDto)commandBus.execute(new GetFile { IdArchivo = id });
            return File(archivo.File, archivo.ContentType, archivo.FileName);
        }

        [HttpPost]
        public IResponse Post(IFormFile file)
        {
            return commandBus.execute(new UploadFile { File = file });
        }

        [HttpPost("registro", Name = "archivoRegistro")]
        public IResponse SubirArchivoRegistro(IFormFile file)
        {
            return commandBus.execute(new UploadRegistrationFile { File = file });
        }
    }
}
