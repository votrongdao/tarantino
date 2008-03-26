using System;
using System.Collections.Generic;
using System.Reflection;
using Tarantino.Core.Commons.Services.ListManagement;
using Tarantino.Core.Commons.Services.ListManagement.Impl;

namespace Tarantino.Core.Commons.Model.Enumerations
{
	public abstract class Enumeration : IComparable
	{
		private int _value;
		private string _displayName;

		protected Enumeration()
		{
		}

		protected Enumeration(int value, string displayName)
		{
			_value = value;
			_displayName = displayName;
		}

		public int Value
		{
			get { return _value; }
		}

		public string DisplayName
		{
			get { return _displayName; }
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public static T FromValue<T>(int value) where T : Enumeration, new()
		{
			IRichList<T> all = new RichList<T>( GetAll<T>());
			T matching = all.Find(delegate(T item) { return item.Value == value; });
			return matching;
		}

		public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
		{
			Type type = typeof(T);
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

			foreach (FieldInfo info in fields)
			{
				T instance = new T();
				T locatedValue = info.GetValue(instance) as T;

				if (locatedValue != null)
				{
					yield return locatedValue;
				}
			}
		}

		public override bool Equals(object obj)
		{
			Enumeration otherValue = (Enumeration)obj;

			bool typeMatches = GetType().Equals(obj.GetType());
			bool valueMatches = _value.Equals(otherValue.Value);

			return typeMatches && valueMatches;
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
		{
			int absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
			return absoluteDifference;
		}

		public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
		{
			IRichList<T> all = new RichList<T>(GetAll<T>());
			T matching = all.Find(delegate(T item) { return item.DisplayName == displayName; });
			return matching;
		}

		public int CompareTo(object other)
		{
			return Value.CompareTo(((Enumeration)other).Value);
		}
	}
}