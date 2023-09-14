using Core;
using DAL;
using Newtonsoft.Json;

namespace BLL { 
    public class QuestionnaireBo : BaseObj<Questionnaire>
    {
		public string Name { 
            get { return Record?.Name; } 
            set { Record.Name = value; } }



        public string Text { get { return Record.Text; } 
            set { Record.Text = value; } }




		List<QuestionBo> _questions;
        public List<QuestionBo> Questions
        {
            get
            {
                if (_questions == null)
                {
                    _questions = new List<QuestionBo>();
                    if (!IsNew) { 
                        var prRep = base._uow.GetRepository<Vjsf>();
                    var questionRecords = prRep.Where(x => x.QuestionnaireId == Id).OrderBy(x => x.Order).ToList();
                    int i = 0;
                    bool nullable = false;
                    foreach (var record in questionRecords)
                    {
                        i++;
                        if (record.Order == null || record.Order != i || nullable)
                        {
                            nullable = true;
                            record.Order = i;
                            prRep.Update(record);
                        }
                        _questions.Add(new QuestionBo(record));
                    } }
                }
                return _questions;
            }
        }




        #region constructors

        public QuestionnaireBo(int? id) : base(id)
        {

         
        }

        public QuestionnaireBo(Questionnaire record) : base(record)
        {

         
        }

        public QuestionnaireBo() : base()
        {
            IsNew = true;

        }

        #endregion
        internal string Error { get; private set; }

        public override void Save(UserDTO user)
        {
            base.Save(user);
            foreach (var question in Questions)
            {
                question.QuestionnaireId = Id;

                question.Save(user);
            }
        }

        public override void Delete(UserDTO user)
        {

            foreach (var question in Questions)
            {
               question.Delete(user);
            }            
            base.Delete(user);

        }


    }

}