using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class StoredFileDto: IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsActive { get; set; }
        public string PhysicalPath { get; set; }
        public int UserId { get; set; }
    }
}
