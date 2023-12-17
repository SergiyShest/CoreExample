using Core;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Entity;

public partial class Answer : IEntity
{
    public int? Id { get; set; }

    public int? QuestionnarieId { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public int? QuestionId { get; set; }

    public DateOnly? Cdate { get; set; }

    public string? SessionId { get; set; }

    public DateTimeOffset? DateTime { get; set; }
}

public partial class vAnswer : IEntity
{
	public int? Id { get; set; }

	public int? QuestionnarieId { get; set; }

	public string? Name { get; set; }

	public string? Value { get; set; }

	public int? QuestionId { get; set; }

	public DateOnly? Cdate { get; set; }
	public string? SessionId { get; set; }

	public string? Time  { get; set; }
	public string? Os { get; set; }
	public string? Area { get; set; }
	public string? Browser { get; set; }
	public string? ScreenSize { get; set; }
}
