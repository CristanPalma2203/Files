using System;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Service
{
    public sealed class R2FileStorageService : IFileStorageService
    {
        private const string RegistrationFolder = "ArchivosRegistroImportadores";

        private readonly IAmazonS3 s3Client;
        private readonly ITokenService token;
        private readonly string bucketName;

        public R2FileStorageService(IAmazonS3 s3Client, IConfiguration configuration, ITokenService token)
        {
            this.s3Client = s3Client;
            this.token = token;
            bucketName = configuration["Storage:R2:BucketName"];

            if (string.IsNullOrWhiteSpace(bucketName))
                throw new InvalidOperationException("Falta configurar Storage:R2:BucketName");
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
            var key = BuildKey(location, identifier);
            using var response = s3Client.GetObjectAsync(bucketName, key).GetAwaiter().GetResult();
            var output = new MemoryStream();
            response.ResponseStream.CopyTo(output);
            output.Position = 0;
            return output;
        }

        private string Save(IFormFile file, string identifier, string folder)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            var safeFolder = RequirePathSegment(folder, nameof(folder));
            var key = BuildKey(safeFolder, identifier);
            using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType,
                DisablePayloadSigning = true,
                DisableDefaultChecksumValidation = true
            };

            s3Client.PutObjectAsync(request).GetAwaiter().GetResult();
            return safeFolder;
        }

        private static string BuildKey(string folder, string identifier)
        {
            return $"{RequireKeyPrefix(folder, nameof(folder))}/{RequirePathSegment(identifier, nameof(identifier))}";
        }

        private static string RequireKeyPrefix(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Contains("\\"))
                throw new ArgumentException("El prefijo debe ser una ruta relativa válida.", parameterName);

            var segments = value.Split('/');
            foreach (var segment in segments)
                RequirePathSegment(segment, parameterName);

            return string.Join("/", segments);
        }

        private static string RequirePathSegment(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value) ||
                value.Contains("/") ||
                value.Contains("\\") ||
                value == "." ||
                value == "..")
            {
                throw new ArgumentException("El valor debe ser un segmento de ruta válido.", parameterName);
            }

            return value;
        }
    }
}
