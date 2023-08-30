

namespace Core
{
	public partial class A_prop: IEntity
    {
        public int? Id { get; set; }
        
        public string? Name { get; internal set; }
    
        public string? Code { get; internal set; }
    }
}

