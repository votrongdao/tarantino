using System.Data;
using StructureMap;

namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IExcelWorksheetReader
	{
		DataTable GetWorksheet(string filePath, string worksheetName);
	}
}