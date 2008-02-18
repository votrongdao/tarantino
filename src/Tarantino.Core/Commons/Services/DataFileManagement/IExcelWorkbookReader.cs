using System.Data;
using System.IO;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Commons.Core.Services.DataFileManagement
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IExcelWorkbookReader
	{
		DataSet GetWorkbookData(Stream file);
	}
}