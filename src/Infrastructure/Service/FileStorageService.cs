using System;
using System.IO;
using Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Service
{
    public class FileStorageService : IFileStorageService
    {
        private const string StorageRootSetting = "AppSettings:RutaAlmacenamiento";
        private const string RegistrationFolder = "ArchivosRegistroImportadores";

        private readonly string storageRoot;
        private readonly ITokenService token;

        public FileStorageService(IConfiguration configuration, ITokenService token)
        {
            var configuredRoot = configuration[StorageRootSetting];
            if (string.IsNullOrWhiteSpace(configuredRoot))
                throw new InvalidOperationException($"Falta configurar {StorageRootSetting}");

            storageRoot = Path.GetFullPath(configuredRoot);
            this.token = token;
        }

        public string Guardar(IFormFile file, string identifier)
        {
            return Save(file, identifier, token.GetUserIdentifier());
        }

        public string GuardarArchivoRegistro(IFormFile file, string identifier)
        {
            return Save(file, identifier, RegistrationFolder);
        }

        public Stream AbrirLectura(string location, string identifier)
        {
            var safeIdentifier = RequirePathSegment(identifier, nameof(identifier));
            var fullPath = Path.GetFullPath(Path.Combine(location, safeIdentifier));
            var relativePath = Path.GetRelativePath(storageRoot, fullPath);

            if (relativePath == ".." ||
                relativePath.StartsWith($"..{Path.DirectorySeparatorChar}", StringComparison.Ordinal) ||
                Path.IsPathRooted(relativePath))
            {
                throw new InvalidOperationException("La ruta del archivo está fuera del almacenamiento configurado.");
            }

            return new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        private string Save(IFormFile file, string identifier, string folder)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            var safeIdentifier = RequirePathSegment(identifier, nameof(identifier));
            var safeFolder = RequirePathSegment(folder, nameof(folder));
            var directory = Path.Combine(storageRoot, safeFolder);

            Directory.CreateDirectory(directory);
            var destination = Path.Combine(directory, safeIdentifier);
            using (var stream = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                file.CopyTo(stream);
            }

            return directory;
        }

        private static string RequirePathSegment(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value) || Path.GetFileName(value) != value)
                throw new ArgumentException("El valor debe ser un segmento de ruta válido.", parameterName);

            return value;
        }
    }
}
