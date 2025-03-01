using System;
using System.Collections.Generic;

namespace Individuellt_projekt___Databaser.Models2;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
