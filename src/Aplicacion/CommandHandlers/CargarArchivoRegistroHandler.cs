using Aplicacion.Commands;
using Aplicacion.Dtos;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using System;

namespace Aplicacion.CommandHandlers
{
    public class CargarArchivoRegistroHandler : AbstractHandler<CargarArchivoRegistro>
    {
        private readonly IArchivoRepository archivoRepository;
        private readonly IGuardarArchivoAlmacenamiento guardarArchivo;
        private readonly IUnitOfWork unitOfWork;

        public CargarArchivoRegistroHandler(IArchivoRepository archivoRepository,
            IGuardarArchivoAlmacenamiento guardarArchivo, IUnitOfWork unitOfWork)
        {
            this.archivoRepository = archivoRepository;
            this.guardarArchivo = guardarArchivo;
            this.unitOfWork = unitOfWork;
        }

        public override IResponse Handle(CargarArchivoRegistro message)
        {
            Guid identificador = Guid.NewGuid();
            string rutaAlmacenamiento = guardarArchivo.GuardarArchivoRegistro(message.File, identificador.ToString());
            Archivo archivo = new Archivo
            {
                Name = message.File.FileName,
                ContentType = message.File.ContentType,
                Identifier = identificador.ToString(),
                PhysicalPath = rutaAlmacenamiento,
                IsActive = true,
                RegisteredAt = DateTime.Now,
            };
            var resultado = archivoRepository.Create(archivo);
            unitOfWork.Save();
            return new OkResponse(resultado.Id);
        }
    }

}
