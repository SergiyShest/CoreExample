using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity;

public partial class User : IEntity
{
    [Key]
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateOnly? Date { get; set; }

    public string? Comment { get; set; }
    
    public string? PasswordHash { get; set; }

    public string? Salt { get; set; }

    public DateTime? Ldate { get; set; }

    public DateTime? Cdate { get; set; }




    public string? Luser { get; set; }

    public string? Cuser { get; set; }

    
}
