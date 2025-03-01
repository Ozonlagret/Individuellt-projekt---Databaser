using System;
using System.Collections.Generic;

namespace Individuellt_projekt___Databaser.Models2;

public partial class Position
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public decimal MonthlySalary { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
