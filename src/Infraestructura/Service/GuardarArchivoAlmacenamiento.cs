using Dominio.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infraestructura.Service
{
    public class GuardarArchivoAlmacenamiento : IGuardarArchivoAlmacenamiento
    {
        private readonly IConfiguration configuration;
        private readonly ITokenService token;

        public GuardarArchivoAlmacenamiento(IConfiguration configuration, ITokenService token)
        {
            this.configuration = configuration;
            this.token = token;

        }

        public string Guardar(IFormFile file, string identificador)
        {
            string carpeta = token.GetIdentificacionUsuario();
            string path = configuration.GetSection("AppSettings").GetSection("RutaAlmacenamiento").Value + carpeta;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(Path.Combine(path, identificador), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return path;
        }

        public string GuardarArchivoRegistro(IFormFile file, string identificador)
        {
            string path = configuration.GetSection("AppSettings").GetSection("RutaAlmacenamiento").Value + "ArchivosRegistroImportadores";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(Path.Combine(path, identificador), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return path;
        }
    }
}

