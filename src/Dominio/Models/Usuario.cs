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
        public string Nombre { get; set; }
        public string IdentificadorAcceso { get; set; }

        public bool Activo { get; set; }
        public string Contrasena { get; set; }
        public int? DepartamentoId { get; set; }
        public bool CambiarContrasena { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public DateTime? FechaActualizacion { get; set; }
        public string TipoUsuario { get; set; }

    }
}
