using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL;

public partial class User : IEntity
{
    [Key]
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }


    public string? Comment { get; set; }

    public string? PasswordHash { get; set; }
        
    public string? Salt { get; set; }


    public DateTime? LDate { get; set; }

    public DateTime? CDate { get; set; }

    public string? LUser { get; set; }

    public string? CUser { get; set; }



}
