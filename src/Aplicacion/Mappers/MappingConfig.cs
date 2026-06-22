using Aplicacion.Dtos;
using Dominio.Models;
using Mapster;

namespace Aplicacion.Mappers
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DtoArchivo, Archivo>().TwoWays();
        }
    }
}
