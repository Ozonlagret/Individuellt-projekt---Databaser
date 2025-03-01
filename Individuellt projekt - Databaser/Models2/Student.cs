using System;
using System.Collections.Generic;

namespace Individuellt_projekt___Databaser.Models2;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public int? ClassId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
