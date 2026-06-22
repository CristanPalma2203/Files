using Aplicacion.Commands;
using Aplicacion.Dtos;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using System;

namespace Aplicacion.CommandHandlers
{
    public class CargarArchivoHandler : AbstractHandler<CargarArchivo>
    {
        private readonly ITokenService token;
        private readonly IArchivoRepository archivoRepository;
        private readonly IGuardarArchivoAlmacenamiento guardarArchivo;
        private readonly IUnitOfWork unitOfWork;

        public CargarArchivoHandler(ITokenService token, IArchivoRepository archivoRepository,
            IGuardarArchivoAlmacenamiento guardarArchivo, IUnitOfWork unitOfWork)
        {
            this.token = token;
            this.archivoRepository = archivoRepository;
            this.guardarArchivo = guardarArchivo;
            this.unitOfWork = unitOfWork;
        }

        public override IResponse Handle(CargarArchivo message)
        {
            int IdUsuario = token.GetIdUsuario();
            Guid identificador = Guid.NewGuid();
            string rutaAlmacenamiento = guardarArchivo.Guardar(message.File, identificador.ToString());
            Archivo archivo = new Archivo
            {
                Nombre = message.File.FileName,
                ContentType = message.File.ContentType,
                Indentificador = identificador.ToString(),
                PathFisico = rutaAlmacenamiento,
                Activo = true,
                FechaRegistro = DateTime.Now,
                UsuarioID = IdUsuario,
            };
            var resultado = archivoRepository.Create(archivo);
            unitOfWork.Save();
            return new OkResponse(resultado.Id);
        }
    }

}
