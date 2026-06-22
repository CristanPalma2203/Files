using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Aplicacion.Dtos
{
    public class DescargarArchivoDto : IResponse
    {

        public Stream File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
