using Core;
using System.ComponentModel.DataAnnotations;

namespace DAL;

public partial class AvaiableUser : IEntity
{
    [Key]
    public int? Id { get; set; }

    public string? Comment { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public DateTime? LDate { get; set; }

    public DateTime? CDate { get; set; }

    public string? LUser { get; set; }

    public string? CUser { get; set; }



}
