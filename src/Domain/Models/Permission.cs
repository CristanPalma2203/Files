using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Permission : IEntity
    {
        public static int idPermisoAdministracion = 1;
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? ParentPermissionId { get; set; }
        public bool IsMenu { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
    }
}
