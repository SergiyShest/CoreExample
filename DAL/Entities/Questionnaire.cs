

namespace Core
{
	public partial class Questionnaire : IEntity
    {
        public int? Id { get; set; }
        
        public string?  Text { get;  set; }
    
        public string?  Name { get;  set; }
    }
}

