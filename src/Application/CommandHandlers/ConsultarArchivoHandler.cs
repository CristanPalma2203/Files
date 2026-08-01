using Application.Commands;
using Application.Dtos;
using Domain.Repositories;
using Domain.Service;

namespace Application.CommandHandlers
{
    public class GetFileHandler : AbstractHandler<GetFile>
    {
        IStoredFileRepository storedFileRepository;
        IFileStorageService fileStorageService;

        public GetFileHandler(IStoredFileRepository storedFileRepository, IFileStorageService fileStorageService) {

            this.storedFileRepository = storedFileRepository;
            this.fileStorageService = fileStorageService;
        }


        public override IResponse Handle(GetFile message)
        {
            var StoredFiles = storedFileRepository.GetById(message.IdArchivo);
            DownloadFileDto datos = new DownloadFileDto
            {
                File = fileStorageService.AbrirLectura(StoredFiles.PhysicalPath, StoredFiles.Identifier),
                FileName = StoredFiles.Name,
                ContentType = StoredFiles.ContentType
            };
            return datos;
        }
    }
}
