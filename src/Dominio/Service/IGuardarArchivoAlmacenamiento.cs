using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dominio.Models.Regla;
using Microsoft.AspNetCore.Http;


namespace Dominio.Service
{
    public interface IGuardarArchivoAlmacenamiento
    {
        string Guardar(IFormFile file, string identificador);
        string GuardarArchivoRegistro(IFormFile file, string identificador);
    }
}
