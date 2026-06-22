using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class OkResponse : IResponse
    {
        public int Identificador{ get; set; }
        public OkResponse(int identificador) {
            this.Identificador = identificador;
        }
    }
}
