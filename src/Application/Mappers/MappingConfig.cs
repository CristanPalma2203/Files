using Application.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Mappers
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<StoredFileDto, StoredFile>().TwoWays();
        }
    }
}
