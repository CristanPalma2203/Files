using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Domain.Models.Rules
{
    public interface IExtensionesPermitidas : IRule
    {
        bool ExtensionValida(string filename);
    }

    public class ExtensionesPermitidas : IExtensionesPermitidas
    {
        private readonly IConfiguration configuration;

        public ExtensionesPermitidas(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool ExtensionValida(string filename)
        {
            var extension = Path.GetExtension(filename);

            string[] extensiones = configuration.GetSection("AppSettings:ExtensionesValidas").Get<string[]>();
            foreach (string permitido in extensiones)
            {
                if (permitido.ToUpper().Equals(extension.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
