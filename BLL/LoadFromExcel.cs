using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DAL.Core;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace BLL
{




	public class ExcelLoader
	{

		HeaderRow HeaderRowDescr;
		public List<string> Errors { get; } = new List<string>();

		public List<QuestionBo> LoadQuestions(string path)
		{
			var questions = new List<QuestionBo>();

			var wb = ExcelWorking.ReadWorkbook(path);
			var sheet = wb.GetSheetAt(wb.ActiveSheetIndex);

			HeaderRowDescr = GetHeaderRow(sheet);
			if (HeaderRowDescr == null) throw new NullReferenceException("not find header row in " + path);

			for (int i = HeaderRowDescr.Index + 1; i < sheet.LastRowNum; i++)
			{
				var row = sheet.GetRow(i);
				if (row == null) continue;
				var question = CreateQuestion(row, ref i);
				if (question != null)
				{ questions.Add(question); }

			}

			return questions;
		}

		public QuestionnaireBo AddQuestions(string path, int questinnaireId)
		{

			var questions = LoadQuestions(path);
			var questionnaire = new QuestionnaireBo(questinnaireId);
			foreach (var quetion in questions)
			{
				questionnaire.Questions.Add(quetion);
			}
			return questionnaire;
		}



		private QuestionBo CreateQuestion(IRow row, ref int nextIndex)
		{
			var questionFirstRowInd = nextIndex;
			var questionLastRowInd = nextIndex;

			var nameCel = row.GetCell(HeaderRowDescr.QuestionGroup.ColumnIndex);
			if (nameCel == null) return null;
			if (nameCel.IsMergedCell)
			{
				var mergedRegion = GetMergedRegionContainingCell(nameCel);
				nextIndex = mergedRegion.LastRow;
				questionLastRowInd = mergedRegion.LastRow;
			}
			var name = getStringValueSafety(row, HeaderRowDescr.QuestionGroup);
			if (string.IsNullOrEmpty(name))
			{
				var f = GetFirstCellInMergedRegionContainingCell(nameCel);
				name = f.StringCellValue;
				if (string.IsNullOrEmpty(name))
				{
					return null;
				}
			}
			QuestionBo question = new QuestionBo();
			try
			{
				question.Name = name;
				var opt = new JsfOptions();
				var schema = new JsfShemaQuestionGroup();
				for (int rowNum = questionFirstRowInd; rowNum <= questionLastRowInd; rowNum++)
				{
					var questionRow=row.Sheet.GetRow(rowNum);
					var text = getStringValueSafety(questionRow, HeaderRowDescr.Text);
					var help = getStringValueSafety(questionRow, HeaderRowDescr.Help);
					var questionType = getStringValueSafety(questionRow, HeaderRowDescr.Type);
					var questionName =  getStringValueSafety(questionRow, HeaderRowDescr.QuestionName);

					if (questionType == "chose")
					{
						var items = new JsfOptionItems() { Name = questionName };
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver1);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver2);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver3);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver4);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver5);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver6);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver7);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver8);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver9);
						AddAnsver(questionRow, items, HeaderRowDescr.Ansver10);
                        opt.Items.Add(items);
					}


					var questionItem = new JsfShemaQuestion(questionName) { Title = text, QuestionType = questionType, Help = help };
					schema.Questions.Add(questionItem);
				}

				question.Schema = schema.Shema;
				question.Options = opt.Options;

			}
			catch (Exception ex)
			{
				Errors.Add(question.Name + ":  " + ex.GetAllMessages());
				question = null;
			}

			return question;
		}

		private void AddAnsver(IRow questionRow, JsfOptionItems items,ICell ansverCell)
		{
			var ansv = getStringValueSafety(questionRow, ansverCell);
			if (!string.IsNullOrWhiteSpace(ansv)) { items.Ansvers.Add(ansv); }
		}

		private HeaderRow GetHeaderRow(ISheet sheet)
		{
			HeaderRow header = null;
			var enumer = sheet.GetRowEnumerator();

			while (enumer.MoveNext())
			{
				var row = enumer.Current as IRow;
				var questionCell = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Trim().ToLower() == "questiongroup");
				var questionNameCell = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Trim().ToLower() == "questionname");
				var textCell = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Trim().ToLower() == "text");
				var typeCell = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Trim().ToLower() == "type");
				if (questionCell != null && questionNameCell!= null && textCell != null && typeCell != null)
				{
					header = new HeaderRow();
					header.Index = row.RowNum;
					header.QuestionGroup = questionCell;
					header.QuestionName = questionNameCell;
					header.Text = textCell;
					header.Type = typeCell;
					header.Ansver1 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver1");
					header.Ansver2 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver2");
					header.Ansver3 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver3");
					header.Ansver4 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver4");
					header.Ansver5 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver5");
					header.Ansver6 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver6");
					header.Ansver7 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver7");
					header.Ansver8 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver8");
					header.Ansver9 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver9");
					header.Ansver10 = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Replace(" ", "").ToLower() == "ansver10");

					header.Help = row.FirstOrDefault<ICell>(x => x.CellType == CellType.String && x.StringCellValue.Trim().ToLower() == "help");
					break;
				}

			}
			return header;

		}

		string getStringValueSafety(IRow row, ICell headerCell)
		{
			try
			{
				var cel = row.GetCell(headerCell.ColumnIndex);

				switch (cel.CellType)
				{
					case CellType.String: return cel.StringCellValue;
					case CellType.Numeric: return cel.NumericCellValue.ToString();
					case CellType.Boolean: return cel.BooleanCellValue.ToString();

				}

				return row.GetCell(headerCell.ColumnIndex).StringCellValue;
			}
			catch { return null; }
		}

		static ICell GetFirstCellInMergedRegionContainingCell(ICell cell)
		{
			if (cell != null && cell.IsMergedCell)
			{
				ISheet sheet = cell.Sheet;
				for (int i = 0; i < sheet.NumMergedRegions; i++)
				{
					CellRangeAddress region = sheet.GetMergedRegion(i);
					if (region.ContainsRow(cell.RowIndex) &&
						region.ContainsColumn(cell.ColumnIndex))
					{
						IRow row = sheet.GetRow(region.FirstRow);
						ICell firstCell = row?.GetCell(region.FirstColumn);
						return firstCell;
					}
				}
				return null;
			}
			return cell;
		}

		static CellRangeAddress GetMergedRegionContainingCell(ICell cell)
		{
			if (cell != null && cell.IsMergedCell)
			{
				ISheet sheet = cell.Sheet;
				for (int i = 0; i < sheet.NumMergedRegions; i++)
				{
					CellRangeAddress region = sheet.GetMergedRegion(i);
					if (region.ContainsRow(cell.RowIndex) &&
						region.ContainsColumn(cell.ColumnIndex))
					{
						return region;
					}
				}

			}
			return null;
		}

	}

	public class JsfOptions
	{
		public List<JsfOptionItems> Items { get; set; } = new List<JsfOptionItems>();

		public dynamic Options
		{
			get
			{
				var opt = getString();
				try
				{

					return JsonConvert.DeserializeObject(opt);

				}
				catch (Exception ex)
				{

					throw;
				}
			}
		}
		public string getString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("{ \"context\":{");

			for (int i = 0; i < Items.Count; i++)
			{
				sb.AppendLine(Items[i].getString());
			}

			sb.AppendLine("}}");

			return sb.ToString();
		}
	}

	public class JsfOptionItems
	{
		public string Name { get; set; }
		public List<string> Ansvers { get; set; } = new List<string>();

		public string getString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"\"{Name}Items\":[");
			for (int i = 0; i < Ansvers.Count; i++)
			{
				sb.AppendLine($"{{ \"Id\":{i + 1},\"Name\":\"{Ansvers[i]}\"}},");
			}
			sb.AppendLine("],");


			return sb.ToString();
		}

	}

	public class JsfShemaQuestionGroup
	{

		public List<JsfShemaQuestion> Questions = new List<JsfShemaQuestion>();

		public dynamic Shema
		{

			get
			{
				var str = getString();
				try
				{
					return JsonConvert.DeserializeObject(str);
				}
				catch (Exception ex)
				{
					throw;
				}
			}
		}

		public string getString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"{{\"type\":\"object\",\"required\":[");
				foreach (var question in Questions)
			{
				sb.AppendLine($"\"{question.OptionsName}\",");
			}
			sb.AppendLine($"],\"properties\":");
			sb.AppendLine("{");
			foreach (var question in Questions)
			{
				sb.AppendLine(question.getString());
			}
			sb.AppendLine("}");
			sb.AppendLine("}");
			return sb.ToString();
		}
	}

	public class JsfShemaQuestion
	{
		public string OptionsName { get; set; }
		public string QuestionType { get; set; }
		public string Title { get; set; }
		public string Help { get; set; }

		public JsfShemaQuestion(string optionsName)
		{
			OptionsName = optionsName;
		}

		public dynamic Item
		{

			get
			{
				var str = getString();
				return JsonConvert.DeserializeObject(str);
			}
		}

		public string getString()
		{
			string template = null;

			switch (QuestionType)
			{
				case "chose":
					template =
			$"			\"{OptionsName}\":{{" +
		   $"				\"type\": \"number\"," +
		   $"				\"title\": \"{Title}\"," +
		   "				\"x-display\": \"radio\"," +
		   $"				\"x-fromData\": \"context.{OptionsName}Items\"," +
		   "				\"x-itemKey\": \"Id\"," +
		   "				\"x-itemTitle\": \"Name\"," +
		   $"				\"description\": \"{Help}\"" +
		   "},";
					break;
				case "string":
					template = $"\"{OptionsName}\":{{\"type\":\"string\",\"title\":\"{Title}\",\"description\":\"{Help}\"}},";
					break;
				case "date":
					template = $"\"{OptionsName}\":{{\"type\":\"string\",\"format\": \"date\",\"title\":\"{Title}\",\"description\":\"{Help}\"}},";
					break;
				case "number":
					template = $"\"{OptionsName}\":{{\"type\":\"string\",\"format\": \"number\",\"title\":\"{Title}\",\"description\":\"{Help}\"}},";
					break;
			}

			return template;
		}
	}

	public class HeaderRow

	{
		public int Index { get; set; }
		public ICell QuestionGroup { get; set; }
		public ICell QuestionName { get; set; }
		public ICell Text { get; set; }
		public ICell Type { get; set; }
		public ICell Ansver1 { get; set; }
		public ICell Ansver2 { get; set; }
		public ICell Ansver3 { get; set; }
		public ICell Ansver4 { get; set; }
		public ICell Ansver5 { get; set; }
		public ICell Ansver6 { get; set; }
		public ICell Ansver7 { get; set; }
		public ICell Ansver8 { get; set; }
		public ICell Ansver9 { get; set; }
		public ICell Ansver10 { get; set; }
		public ICell Help { get; set; }

	}


}
