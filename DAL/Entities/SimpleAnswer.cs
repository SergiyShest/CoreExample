using Core;

namespace Entity;

public partial class SimpleAnswer:IEntity {

    public int? Id { get; set; }

    public int? QuestionnarieId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPhone { get; set; }

    public string? Note { get; set; }

    public string? SessionId { get; set; }

    public DateOnly? Cdate { get; set; }


    public DateTimeOffset? DateTime { get; set; }
}
