using Application.Commands;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Domain.Service;
using System;

namespace Application.CommandHandlers
{
    public class UploadFileHandler : AbstractHandler<UploadFile>
    {
        private readonly ITokenService token;
        private readonly IStoredFileRepository storedFileRepository;
        private readonly IFileStorageService guardarArchivo;
        private readonly IUnitOfWork unitOfWork;

        public UploadFileHandler(ITokenService token, IStoredFileRepository storedFileRepository,
            IFileStorageService guardarArchivo, IUnitOfWork unitOfWork)
        {
            this.token = token;
            this.storedFileRepository = storedFileRepository;
            this.guardarArchivo = guardarArchivo;
            this.unitOfWork = unitOfWork;
        }

        public override IResponse Handle(UploadFile message)
        {
            int IdUsuario = token.GetUserId();
            Guid identifier = Guid.NewGuid();
            string rutaAlmacenamiento = guardarArchivo.Guardar(message.File, identifier.ToString());
            StoredFile StoredFiles = new StoredFile
            {
                Name = message.File.FileName,
                ContentType = message.File.ContentType,
                Identifier = identifier.ToString(),
                PhysicalPath = rutaAlmacenamiento,
                IsActive = true,
                RegisteredAt = DateTime.Now,
                UserId = IdUsuario,
            };
            var resultado = storedFileRepository.Create(StoredFiles);
            unitOfWork.Save();
            return new OkResponse(resultado.Id);
        }
    }

}
