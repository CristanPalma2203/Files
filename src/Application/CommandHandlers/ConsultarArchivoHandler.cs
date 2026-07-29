using Application.Commands;
using Application.Dtos;
using Domain.Repositories;
using System;
using System.IO;

namespace Application.CommandHandlers
{
    public class GetFileHandler : AbstractHandler<GetFile>
    {
        IStoredFileRepository storedFileRepository;

        public GetFileHandler(IStoredFileRepository storedFileRepository) {

            this.storedFileRepository = storedFileRepository;
        }


        public override IResponse Handle(GetFile message)
        {
            var StoredFiles = storedFileRepository.GetById(message.IdArchivo);
            var dataBytes = File.ReadAllBytes(StoredFiles.PhysicalPath + "/" + StoredFiles.Identifier);
            var dataStream = new MemoryStream(dataBytes);
            DownloadFileDto datos = new DownloadFileDto
            {
                File = dataStream,
                FileName = StoredFiles.Name,
                ContentType = StoredFiles.ContentType
            };
            return datos;
        }
    }
}
