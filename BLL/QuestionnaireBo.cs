using Core;
using DAL;
using Sasha.Lims.WebUI.Areas.Questions;

namespace BLL { 
    public class QuestionnaireBo : BaseObj<Questionnaire>
    {
        public string? Code { get; set; }

        List<QuestionBo> _questions;
        public List<QuestionBo> Questions
        {
            get
            {
                if (_questions == null)
                {
                    _questions = new List<QuestionBo>();
                    // var prRep = base._wfWorkService.Database.GetRepository<a_prop>();
                    // var questionRecords = prRep.Where(x => x.prop_id == Id).OrderBy(x => x.num).ToList();
                    //int i = 0;
                    //bool nullable = false;
                    //foreach (var record in questionRecords)
                    //{
                    //    i++;
                    //    if (record.num == null || record.num != i || nullable)
                    //    {
                    //        nullable = true;
                    //        record.num = i;
                    //        prRep.Update(record);
                    //        //--prRep.Save();
                    //    }
                    //    _questions.Add(new Question(record));
                    //}
                }
                return _questions;
            }
        }
        #region constructors

        public QuestionnaireBo(int? id) : base(id)
        {

            Code = "QUESTIONNAIRE";
        }

        public QuestionnaireBo(Questionnaire record) : base(record)
        {

            Code = "QUESTIONNAIRE";
        }

        public QuestionnaireBo() : base()
        {

            Code = "QUESTIONNAIRE";
        }

        #endregion
        internal string Error { get; private set; }
        public string Name { get; internal set; }
        public int? ParentId { get; internal set; }

        public override void Save(UserDTO user)
        {
            base.Save(user);
            foreach (var question in Questions)
            {
                question.QuestionnaireId = Id;

                question.Save(user);
            }
        }

    }

}