using System;
using System.Collections.Generic;

namespace Individuellt_projekt___Databaser.Models2;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? FirstName { get; set; }

    public string? Surname { get; set; }

    public int? PositionId { get; set; }

    public DateOnly? HireDate { get; set; }

    public int? DepartmentId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Position? Position { get; set; }
}
