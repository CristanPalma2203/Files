using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class OkResponse : IResponse
    {
        public int Identifier{ get; set; }
        public OkResponse(int identifier) {
            this.Identifier = identifier;
        }
    }
}
