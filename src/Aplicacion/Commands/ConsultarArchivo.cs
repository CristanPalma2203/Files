using Aplicacion.Common;

namespace Aplicacion.Commands
{
    public class ConsultarArchivo : IAppMessage
    {
        public int IdArchivo { get; set; }
    }
}
