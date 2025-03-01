using System;
using System.Collections.Generic;

namespace Individuellt_projekt___Databaser.Models2;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
