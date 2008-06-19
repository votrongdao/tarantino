using System;
using System.Data;
using Tarantino.Core.Commons.Model.Enumerations;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	public class EnumerationType<T> : ValueTypeType where T : Enumeration, new()
	{
		public EnumerationType() : base(new SqlType(DbType.Int32))
		{
		}

		public override object Get(IDataReader rs, int index)
		{
			var value = (int) rs[index];
			return Enumeration.FromValue<T>(value);
		}

		public override object Get(IDataReader rs, string name)
		{
			int ordinal = rs.GetOrdinal(name);
			return Get(rs, ordinal);
		}

		public override Type ReturnedClass
		{
			get { return typeof (T); }
		}

		public override string ObjectToSQLString(object value)
		{
			return value.ToString();
		}

		public override object FromStringValue(string xml)
		{
			return int.Parse(xml);
		}

		public override string Name
		{
			get { return "Enumeration"; }
		}

		public override void Set(IDbCommand cmd, object value, int index)
		{
			var parameter = (IDataParameter) cmd.Parameters[index];

			var val = (Enumeration)value;

			parameter.Value = val.Value;
		}

		public override bool Equals(object x, object y)
		{
			//get boxed values.
			if (x == null && y == null)
				return true;
			
			if (x == null || y == null)
				return false;

			var xTyped = (Enumeration)x;
			var yTyped = (Enumeration)y;

			return xTyped.Equals(yTyped);
		}
	}
}