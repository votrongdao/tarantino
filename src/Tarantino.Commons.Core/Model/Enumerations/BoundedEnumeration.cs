using System;
using System.Collections.Generic;

namespace Tarantino.Commons.Core.Model.Enumerations
{
	public class BoundedEnumeration<T> : Enumeration
	{
		private T _lower;
		private T _upper;

		public BoundedEnumeration()
		{
		}

		public BoundedEnumeration(int value, string displayName, T lower, T upper) : base(value, displayName)
		{
			_lower = lower;
			_upper = upper;
		}

		public T Lower
		{
			get { return _lower; }
		}

		public T Upper
		{
			get { return _upper; }
		}

		public static K FromBoundedValue<K>(T boundedValue) where K : BoundedEnumeration<T>, new()
		{
			List<K> enumerationValues = new List<K>(GetAll<K>());

			K foundEnumerationValue = enumerationValues.Find(
				delegate(K val)
					{
						return (Convert.ToDecimal(boundedValue) >= Convert.ToDecimal(val.Lower) && Convert.ToDecimal(boundedValue) <= Convert.ToDecimal(val.Upper));
					});

			return foundEnumerationValue;
		}
	}
}