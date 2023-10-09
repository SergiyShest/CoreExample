

using System.ComponentModel.DataAnnotations;

namespace Core
{

    public partial class Vjsf : IEntity 
    {
        [Key]
        public int? Id { get; set; }

        public int? QuestionnaireId { get; set; }
        
        public string? Name { get;  set; }
    
        public string? Code { get; set; }

        public string? Schema { get; set; }

        public string? Options { get; set; }

        public int? Order { get; set; }

		public string? Description { get; set; }

        public string? NextQuestionCondition { get; set; }

        public bool ShowNexButton { get; set; } = true;
        
        public string? NextButtonText { get; set; }

        public bool ShowPrevButton { get; set;} = true;

        public string? PrevButtonText { get; set; }
        
        public string? CssStyle  { get; set; }

    }

}

