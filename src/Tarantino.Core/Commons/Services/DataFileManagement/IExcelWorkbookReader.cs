using System.Data;
using System.IO;
using StructureMap;

namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IExcelWorkbookReader
	{
		DataSet GetWorkbookData(Stream file);
	}
}