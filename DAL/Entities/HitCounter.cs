using Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity;

public partial class HitCounter:IEntity
{
    public int? Id { get; set; }

    public string? SessionId { get; set; }

    public int? QuestionnaireId { get; set; }

    public string ScreenSize { get; set; }

    public string Browser { get; set; }

    public string BrowserVersion { get; set; }

    public string Mobile { get; set; }
   
    public string Os { get; set; }

    public string OsVersion { get; set; }

    public string Cookies { get; set; }

    public string FlashVersion { get; set; }

    public string Area { get; set; }

    public DateTime? Cdate { get; set; }

}
