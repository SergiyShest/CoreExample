using Core;
using System;
using System.Collections.Generic;

namespace Entity;

public partial class QuestionImage:IEntity
{
    public int? Id { get; set; }

    public int? QuestionId { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set; }

    public string? CssStyle { get; set; }
}
