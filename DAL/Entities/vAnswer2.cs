using Core;

namespace Entity;

public partial class vAnswer2 : IEntity
{
    public int? Id { get; set; }

    public int? QuestionnarieId { get; set; }

    //public bool? Answer { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPhone { get; set; }

    public string? Note { get; set; }

    public DateOnly? Cdate { get; set; }
    public string? SessionId { get; set; }

    public string? Time { get; set; }
    public string? Os { get; set; }
    public string? Area { get; set; }
    public string? Browser { get; set; }
    public string? ScreenSize { get; set; }
}
