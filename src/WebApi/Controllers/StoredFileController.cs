using Application.Commands;
using Application.Dtos;
using Application.Services.Comandos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    // Dual prefix: StoredFile (EN) + Archivo (legacy clients / thumbs).
    // No route Names — dual templates cannot share the same Name.
    [Route("api/[controller]")]
    [Route("api/Archivo")]
    [ApiController]
    public class StoredFileController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public StoredFileController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpGet("{id}")]
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

        [HttpPost("registro")]
        public IResponse SubirArchivoRegistro(IFormFile file)
        {
            return commandBus.execute(new UploadRegistrationFile { File = file });
        }
    }
}
