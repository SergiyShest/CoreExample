using Core;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public partial class Answer : IEntity
    {
        public int? Id { get; set; }

        public int? QuestionnarieId {get;  set; }

		public int? QuestionId { get; set; }

		public string? SessionId { get;  set; }

		public string? Name { get;  set; }
    
        public string? Value { get;  set; }

        public DateTime? Cdate { get;  set; }
    }

}

