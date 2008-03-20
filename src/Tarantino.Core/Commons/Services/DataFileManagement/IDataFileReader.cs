using System;
using Tarantino.Core.Commons.Model.Enumerations;
using StructureMap;

namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	[PluginFamily(Keys.Default)]
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