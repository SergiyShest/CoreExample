using Core;

namespace Sasha.Lims.WebUI.Areas.Questions
{
    public partial class a_comment : IEntity
    {

        public int? id { get; set; }
        public int? Id { get; set; }
        public string comment { get; internal set; }
    }
 }

