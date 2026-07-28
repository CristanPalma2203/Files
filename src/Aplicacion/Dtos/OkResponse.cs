using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class OkResponse : IResponse
    {
        public int Identifier{ get; set; }
        public OkResponse(int identificador) {
            this.Identifier = identificador;
        }
    }
}
