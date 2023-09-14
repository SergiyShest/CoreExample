using System;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace BLL
{
	public class ExcelWorking
	{


		public static IWorkbook ReadWorkbook(string path)
		{
			IWorkbook book = null;
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("filePath");
			}

			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Could not find file", path);
			}
			try
			{
				FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				// Try to read workbook as XLSX:
				try
				{
					book = new XSSFWorkbook(fs);
				}
				catch
				{
					book = null;
				}

				// If reading fails, try to read workbook as XLS:
				if (book == null)
				{
					book = new HSSFWorkbook(fs);
				}
			}
			catch (Exception ex)
			{

				throw;
			}
			return book;
		}



	}
}
