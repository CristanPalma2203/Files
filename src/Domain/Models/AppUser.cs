using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AppUser : IEntity
    {

        public static string adminUserEmail = "admin";
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccessIdentifier { get; set; }

        public bool IsActive { get; set; }
        public string Password { get; set; }
        public int? DepartmentId { get; set; }
        public bool MustChangePassword { get; set; }

        public DateTime? RegisteredAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UserType { get; set; }

    }
}
