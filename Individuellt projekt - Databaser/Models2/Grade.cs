using System;
using System.Collections.Generic;

namespace Individuellt_projekt___Databaser.Models2;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? CourseId { get; set; }

    public int? StudentId { get; set; }

    public string? Grade1 { get; set; }

    public int? StaffId { get; set; }

    public DateOnly? DateAssigned { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual Student? Student { get; set; }
}
