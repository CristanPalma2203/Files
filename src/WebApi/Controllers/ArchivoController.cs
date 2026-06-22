using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Commands;
using Aplicacion.Dtos;
using Aplicacion.Services.Comandos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Dominio.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
         public ICommandBus CommandBus { get; }

        public ArchivoController(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

        // GET: api/Archivo/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var archivo = (DescargarArchivoDto)CommandBus.execute(new ConsultarArchivo { IdArchivo = id });
            return File(archivo.File,archivo.ContentType,archivo.FileName);
        }

        // POST: api/Archivo
        [HttpPost]
        public IResponse Post(IFormFile file)
        {
            var resultado = CommandBus.execute(new CargarArchivo { File = file});
            return resultado;
        }



        // POST: api/Archivo
        [HttpPost("registro", Name ="archivoRegistro")]
        public IResponse SubirArchivoRegistro(IFormFile file)
        {
            var resultado = CommandBus.execute(new CargarArchivoRegistro { File = file });
            return resultado;
        }


    }
}
