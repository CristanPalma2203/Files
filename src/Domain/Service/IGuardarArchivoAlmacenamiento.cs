using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Domain.Models.Rules;
using Microsoft.AspNetCore.Http;


namespace Domain.Service
{
    public interface IFileStorageService
    {
        string Guardar(IFormFile file, string identifier);
        string GuardarArchivoRegistro(IFormFile file, string identifier);
        Stream AbrirLectura(string location, string identifier);
    }
}
