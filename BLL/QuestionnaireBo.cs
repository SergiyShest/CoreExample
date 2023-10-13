using Core;
using DAL;
using DAL.Core;
using DAL.Migrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BLL
{

    public class QuestionnaireBo : BaseObj<Questionnaire>
    {
        #region Properties

        #region Name
        public string Name
        {
            get { return Record?.Name; }
            set { Record.Name = value; }
        }
        #endregion

        #region Text

        public string Text
        {
            get { return Record.Text; }
            set { Record.Text = value; }
        }
        #endregion

        #region Main

        public bool? Main
        {
            get { return Record.Main; }
            set { Record.Main = value; }
        }
        #endregion

        #region CssStyle
        public dynamic CssStyle
        {
            get
            {
                if (Record.CssStyle == null)
                    return null;
                return JsonConvert.DeserializeObject(Record.CssStyle);
            }
            set
            {
                if (value != null && value.GetType() == typeof(JObject))
                {
                    Record.CssStyle = value.ToString();
                }
                else
                {
                    Record.CssStyle = value;
                }
            }
        }
        #endregion

        #region Questions


List<QuestionBo> SavedQuestions
        {
            get {
				var questions = new List<QuestionBo>();
				var prRep = base.Uow.GetRepository<Vjsf>();
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
                            questions.Add(new QuestionBo(record, base.Uow));
                        }
                        return questions;
            }
        }



		List<QuestionBo> _questions;
        public List<QuestionBo> Questions
        {
            get
            {
                if (_questions == null)
                {
                    _questions = new List<QuestionBo>();
                    if (!IsNew)
                    {
                        _questions = SavedQuestions;
					}
                }
                return _questions;
            }

            set { _questions = value; }
        }

        #endregion

        internal string Error { get; private set; }

        #endregion

        #region constructors

        public QuestionnaireBo( int? id) : base(id)
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

        #region Methods

        public override void Save(IUnitOfWorkEx uow, UserDTO user, bool withSave = false)
        {
            this.Uow = uow;
            if (Main == true)
            {
                var prevMain = uow.GetRepository<Questionnaire>().FirstOrDefault(x => x.Main == true && x.Id != Id, false);
                if (prevMain != null)
                {
                    prevMain.Main = false;
                }
            }
            base.Save(uow, user, false);

			var forDelete = new List<QuestionBo>();
			foreach (var image in SavedQuestions)
			{
				if (!Questions.Any(x => x.Id == image.Id))
				{
					forDelete.Add(image);
				}
			}

			foreach (var bo in forDelete)
			{
				if (bo.Id != null)
					bo.Delete(uow, user, withSave);
			}



			foreach (var question in Questions)
            {  if (!SavedQuestions.Any(x => x.Id == question.Id)){ question.Id = null; }
                question.QuestionnaireId = Id;
                question.Save(uow, user, false);
            }
            base.Uow.Save();
        }

        public override void Delete(IUnitOfWorkEx uow, UserDTO user, bool withSave = false)
        {
            this.Uow = uow;

            foreach (var question in Questions)
            {
                question.Delete(uow,user, withSave);
            }
            base.Delete(uow,user, withSave);
            base.Uow.Save();
        }

        #endregion

    }

}