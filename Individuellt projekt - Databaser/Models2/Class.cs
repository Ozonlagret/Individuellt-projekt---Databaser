using System;
using System.Collections.Generic;

namespace Individuellt_projekt___Databaser.Models2;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int? MentorId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Staff? Mentor { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
