using Dominio.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class Usuario : IEntity
    {

        public static string correoUsuarioAdmin = "administrador@senasa.gob.sv";
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccessIdentifier { get; set; }

        public bool IsActive { get; set; }
        public string Password { get; set; }
        public int? DepartamentoId { get; set; }
        public bool MustChangePassword { get; set; }

        public DateTime? RegisteredAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UserType { get; set; }

    }
}
