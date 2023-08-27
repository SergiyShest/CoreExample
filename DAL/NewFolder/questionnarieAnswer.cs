using Core;

namespace Sasha.Lims.WebUI.Areas.Questions
{
    public partial class questionnarieAnswer : IEntity
    { 
        public int? Id { get; set; }
        
        public string? Name { get;  set; }
    
        public string? Code { get;  set; }

        public string? Value { get;  set; }

    }

}

