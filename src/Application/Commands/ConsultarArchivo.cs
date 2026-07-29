using Application.Common;

namespace Application.Commands
{
    public class GetFile : IAppMessage
    {
        public int IdArchivo { get; set; }
    }
}
