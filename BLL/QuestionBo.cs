using Core;
using DAL;
using DAL.Core;
using Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text.Json.Nodes;

namespace BLL
{
	[JsonObject]
	public class QuestionBo : BaseObj<Vjsf>
	{
		IUnitOfWorkEx _uow;
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
			get { return Record?.Code; }
			set { Record.Code = value; }
		}
		#endregion

		#region Order
		public int? Order
		{
			get { return Record?.Order; }
			set { Record.Order = value; }
		}
		#endregion

		#region NextQuestionCondition
		public string? NextQuestionCondition
		{
			get { return Record?.NextQuestionCondition; }
			set { Record.NextQuestionCondition = value; }
		}
		#endregion

		#region NextButtonText
		public string? NextButtonText
		{
			get { return Record?.NextButtonText; }
			set { Record.NextButtonText = value; }
		}

		#endregion

		#region PrevButtonText
		public string? PrevButtonText
		{
			get { return Record?.PrevButtonText; }
			set { Record.PrevButtonText = value; }
		}

		#endregion

		#region ShowNextButton
		public bool ShowNextButton
		{
			get { return Record.ShowNexButton==true; }
			set { Record.ShowNexButton = value; }
		}

		#endregion

		#region ShowPrevButton
		public bool ShowPrevButton
		{
			get { return Record.ShowPrevButton==true; }
			set { Record.ShowPrevButton = value; }
		}

		#endregion


		#region QuestionImages

		List<QuestionImage> _savedQuestionImages;
		List<QuestionImage> SavedQuestionImages
		{
			get
			{
				if (_savedQuestionImages == null)
					_savedQuestionImages = new List<QuestionImage>();
				if (_uow != null)
				{
					var images = _uow.GetRepository<QuestionImage>().Where(x => x.QuestionId == Id).ToList();
					if (images.Any())
						_savedQuestionImages = images;
				}
				return _savedQuestionImages;
			}

		}


		List<QuestionImage> _questionImages;

		public List<QuestionImage> QuestionImages
		{
			get
			{
				if (_questionImages == null)
					_questionImages = SavedQuestionImages;
				return _questionImages;
			}

			set
			{
				_questionImages = value;

			}
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

		#region Schema

		public dynamic Schema
		{
			get
			{
				if (Record.Schema == null)
					Record.Schema = getDefaultShema();
				return JsonConvert.DeserializeObject(Record.Schema);
			}
			set
			{
				if (value != null && value.GetType() == typeof(JObject))
				{
					Record.Schema = value.ToString();
				}
				else
				{
					Record.Schema = value;
				}
			}
		}

		private string getDefaultShema()
		{
			dynamic dynObj = new ExpandoObject();
			dynObj.type = "object";
			dynObj.properties = new ExpandoObject();
			dynamic ListProp = new ExpandoObject();

			ListProp.type = "number";
			ListProp.title = "Choose the right Ansver";
			ListProp.x_display = "radio";
			ListProp.x_fromData = "context.Items";
			ListProp.x_itemKey = "Id";
			ListProp.x_itemTitle = "Name";
			ListProp.description = "This description is used as a help message.";

			dynObj.properties.ListProp = ListProp;
			return JsonConvert.SerializeObject(dynObj);
		}

		#endregion

		#region Options

		public dynamic Options
		{
			get
			{
				if (Record.Options == null)
					Record.Options = getDefaultOptions();
				return JsonConvert.DeserializeObject(Record.Options);
			}
			set
			{
				if (value != null && value.GetType() == typeof(JObject))
				{
					Record.Options = value.ToString();
				}
				else
					Record.Options = value;
			}
		}

		private string getDefaultOptions()
		{
			dynamic dynObj = new ExpandoObject();
			dynamic context = new ExpandoObject();
			context.Items = new List<NamedInt>()
			{
				new NamedInt { Id = 1, Name = "Ansver first" },
				new NamedInt { Id = 2, Name = "Ansver second" },
				new NamedInt { Id = 3, Name = "Ansver next" }
			};
			dynObj.context = context;
			dynObj.selectAll = true;
			return JsonConvert.SerializeObject(dynObj);
		}

		#endregion

		#region QuestionnaireId

		public int? QuestionnaireId
		{
			get { return Record.QuestionnaireId; }
			set { Record.QuestionnaireId = value; }
		}

		#endregion

		#region Description

		public string Description
		{
			get { return Record?.Description; }
			set { Record.Description = value; }
		}

		#endregion

		internal string Error { get; private set; }

		#endregion

		#region Constructors
		public QuestionBo(int? id)
		{
			this.Id = id;
		}
		public QuestionBo(Vjsf record, IUnitOfWorkEx uow) : base(record)
		{
			_uow = uow;
		}
		public QuestionBo() : base()
		{

		}
		#endregion

		public override void Save(IUnitOfWorkEx uow, UserDTO user, bool withSave = true)
		{

			if (Id <= 0)
			{
				SetId(0);
			}
			base.Save(uow, user, withSave);

			var forDelete = new List<QuestionImage>();
			foreach (var image in SavedQuestionImages)
			{
				if (!QuestionImages.Any(x => x.Id == image.Id))
				{
					forDelete.Add(image);
				}
			}

			foreach (var image in forDelete)
			{    if(image.Id!=null)
				uow.Delete<QuestionImage>((int)image.Id, withSave);
			} 

			foreach (var image in QuestionImages)
			{
				if (!SavedQuestionImages.Any(x => x.Id == image.Id))
				{ image.Id = null; }
				image.QuestionId = Id;
				uow.Save(image);
			}

		}

		public override void Delete(IUnitOfWorkEx uow, UserDTO user, bool withSave = true)
		{
			if (QuestionImages != null)
				foreach (var image in QuestionImages)
				{  
					if(image.Id!=null)
					uow.Delete<QuestionImage>((int)image.Id, withSave);
				}
			base.Delete(uow, user, withSave);
		}
	}
}

