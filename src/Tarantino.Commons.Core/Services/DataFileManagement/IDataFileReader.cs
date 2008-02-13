using System;
using Tarantino.Commons.Core.Model.Enumerations;
using StructureMap;

namespace Tarantino.Commons.Core.Services.DataFileManagement
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IDataFileReader : IDisposable
	{
		void Open(string assembly, string resourceFilename, string filePath);
		bool Read();
		int GetInteger(string columnName);
		T GetEnumerationByDisplayName<T>(string columnName) where T : Enumeration, new();
		T GetEnumerationByValue<T>(string columnName) where T : Enumeration, new();
		string GetString(string columnName);
		string[] GetColumnHeaders();
	}
}