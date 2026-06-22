using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class DtoArchivo: IResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool Activo { get; set; }
        public string PathFisico { get; set; }
        public int UsuarioId { get; set; }
    }
}
