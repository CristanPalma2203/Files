using Aplicacion.Commands;
using Aplicacion.Dtos;
using Dominio.Repositories;
using System;
using System.IO;

namespace Aplicacion.CommandHandlers
{
    public class ConsultarArchivoHandler : AbstractHandler<ConsultarArchivo>
    {
        IArchivoRepository archivoRepository;

        public ConsultarArchivoHandler(IArchivoRepository archivoRepository) {

            this.archivoRepository = archivoRepository;
        }


        public override IResponse Handle(ConsultarArchivo message)
        {
            var archivo = archivoRepository.GetById(message.IdArchivo);
            var dataBytes = File.ReadAllBytes(archivo.PathFisico + "/" + archivo.Indentificador);
            var dataStream = new MemoryStream(dataBytes);
            DescargarArchivoDto datos = new DescargarArchivoDto
            {
                File = dataStream,
                FileName = archivo.Nombre,
                ContentType = archivo.ContentType
            };
            return datos;
        }
    }
}
