

using System.ComponentModel.DataAnnotations;

namespace Core
{
    public partial class QuestionImage : IEntity 
    {
        [Key]
        public int? Id { get; set; }

        public int? QuestionId { get; set; }
        
        public string? Name { get;  set; }

        public string? Path { get; set; }
       
        public string? CssStyle  { get; set; }

    }

}

