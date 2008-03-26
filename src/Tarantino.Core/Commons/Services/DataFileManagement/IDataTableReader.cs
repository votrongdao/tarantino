using System.Data;
using Tarantino.Core.Commons.Model.Enumerations;
using StructureMap;

namespace Tarantino.Core.Commons.Services.DataFileManagement
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