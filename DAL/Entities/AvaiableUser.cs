using Core;
using System;
using System.Collections.Generic;

namespace Entity;

public partial class AvaiableUser :IEntity
{
    public int? Id { get; set; }

    public string? Comment { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public DateTime? Ldate { get; set; }

    public DateTime? Cdate { get; set; }

    public string? Luser { get; set; }

    public string? Cuser { get; set; }
}
