using System.Data;
using Tarantino.Commons.Core.Model.Enumerations;
using StructureMap;

namespace Tarantino.Commons.Core.Services.DataFileManagement
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDataTableReader
	{
		void Open(DataTable table);

		bool Read();

		string GetString(string columnName);
		int GetInteger(string columnName);
		decimal GetDecimal(string columnName);
		T GetEnumeration<T>(string columnName) where T : Enumeration, new();
		bool GetBoolean(string columnName);
	}
}