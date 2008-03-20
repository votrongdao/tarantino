using System;
using System.Data;
using Tarantino.Core.Commons.Model.Enumerations;
using StructureMap;

namespace Tarantino.Core.Commons.Services.DataFileManagement.Impl
{
	[Pluggable(Keys.Default)]
	public class DataTableReader : IDataTableReader
	{
		private DataTable _table;
		private int _currentRowIndex = -1;

		public void Open(DataTable table)
		{
			_table = table;
		}

		public bool Read()
		{
			int numberOfRows = _table.Rows.Count;
			_currentRowIndex++;
			bool tableHasEnoughRows = numberOfRows > _currentRowIndex;

			bool canRead = tableHasEnoughRows && currentRowHasValues();

			return canRead;
		}

		private bool currentRowHasValues()
		{
			bool hasValues = false;
			foreach (DataColumn column in _table.Columns)
			{
				string currentColumnValue = _table.Rows[_currentRowIndex][column.ColumnName].ToString();
				
				if (!string.IsNullOrEmpty(currentColumnValue))
				{
					hasValues = true;
				}
			}
			return hasValues;
		}

		public int GetInteger(string columnName)
		{
			int value = int.Parse(GetString(columnName));
			return value;
		}

		public decimal GetDecimal(string columnName)
		{
			decimal value = decimal.Parse(GetString(columnName));
			return value;
		}

		public T GetEnumeration<T>(string columnName) where T : Enumeration, new()
		{
			string displayName = GetString(columnName);
			T value = Enumeration.FromDisplayName<T>(displayName);
			
			if (value == null)
			{
				string message = string.Format("'{0}' is not a valid value in {1}", displayName, typeof(T));
				throw new ApplicationException(message);
			}

			return value;
		}

		public bool GetBoolean(string columnName)
		{
			bool value = bool.Parse(GetString(columnName));
			return value;
		}

		public string GetString(string columnName)
		{
			string currentRowValue = _table.Rows[_currentRowIndex][columnName] as string;
			
			if (currentRowValue == string.Empty)
			{
				currentRowValue = null;
			}

			return currentRowValue;
		}
	}
}