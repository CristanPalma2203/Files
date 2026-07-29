using Application.Commands;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Domain.Service;
using System;

namespace Application.CommandHandlers
{
    public class UploadRegistrationFileHandler : AbstractHandler<UploadRegistrationFile>
    {
        private readonly IStoredFileRepository storedFileRepository;
        private readonly IFileStorageService guardarArchivo;
        private readonly IUnitOfWork unitOfWork;

        public UploadRegistrationFileHandler(IStoredFileRepository storedFileRepository,
            IFileStorageService guardarArchivo, IUnitOfWork unitOfWork)
        {
            this.storedFileRepository = storedFileRepository;
            this.guardarArchivo = guardarArchivo;
            this.unitOfWork = unitOfWork;
        }

        public override IResponse Handle(UploadRegistrationFile message)
        {
            Guid identifier = Guid.NewGuid();
            string rutaAlmacenamiento = guardarArchivo.GuardarArchivoRegistro(message.File, identifier.ToString());
            StoredFile StoredFiles = new StoredFile
            {
                Name = message.File.FileName,
                ContentType = message.File.ContentType,
                Identifier = identifier.ToString(),
                PhysicalPath = rutaAlmacenamiento,
                IsActive = true,
                RegisteredAt = DateTime.Now,
            };
            var resultado = storedFileRepository.Create(StoredFiles);
            unitOfWork.Save();
            return new OkResponse(resultado.Id);
        }
    }

}
