using Core;

namespace Entity;

public partial class AnswerNote:IEntity {

    public int? Id { get; set; }

    public int? AnswerId { get; set; }

    public string? Note { get; set; }

    public int? LUser { get; set; }

    public int? CUser { get; set; }

    public DateTime? Ldate { get; set; }

    public DateTime? Cdate { get; set; }

}