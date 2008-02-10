using Tarantino.Commons.Core.Model.Enumerations;

namespace Tarantino.Commons.Core.Model.Repositories
{
	public class Criterion
	{
		private string _attribute;
		private object _value;
		private ComparisonOperator _operator;

		public Criterion()
		{
		}

		public Criterion(string attribute, object value, ComparisonOperator comparisonOperator)
		{
			_attribute = attribute;
			_value = value;
			_operator = comparisonOperator;
		}

		public Criterion(string attribute, object value) : this(attribute, value, ComparisonOperator.Equal)
		{
		}

		public string Attribute
		{
			get { return _attribute; }
			set { _attribute = value; }
		}

		public object Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public ComparisonOperator Operator
		{
			get { return _operator; }
			set { _operator = value; }
		}
	}
}