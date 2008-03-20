using System.Data;
using StructureMap;

namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	[PluginFamily(Keys.Default)]
	public interface IExcelWorksheetReader
	{
		DataTable GetWorksheet(string filePath, string worksheetName);
	}
}