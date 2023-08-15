using System;

namespace MinimalCodeFirst.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public required string Name { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
