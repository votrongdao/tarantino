using System;
using System.Collections.Generic;
using Tarantino.Commons.Core.Model.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Commons.Core.Model.Enumerations
{
	[TestFixture]
	public class EnumerationTester
	{
		[Test]
		public void Should_correctly_determine_absolute_difference()
		{
			Assert.That(Enumeration.AbsoluteDifference(PersistenceMode.Archive, PersistenceMode.Live), Is.EqualTo(1));
		}

		[Test]
		public void Should_determine_enumerations_of_different_type_arent_equal()
		{
			Assert.That(PersistenceMode.Archive, Is.Not.EqualTo(2));
		}

		[Test]
		public void Should_compare_correctly_for_equality()
		{
			Assert.That(PersistenceMode.Live.CompareTo(PersistenceMode.Archive), Is.EqualTo(-1));
			Assert.That(PersistenceMode.Archive.CompareTo(PersistenceMode.Live), Is.EqualTo(1));
			Assert.That(PersistenceMode.Live.CompareTo(PersistenceMode.Live), Is.EqualTo(0));
		}

		[Test]
		public void Can_sort_enumeration_values()
		{
			PersistenceMode[] modes = new PersistenceMode[] {PersistenceMode.Archive, PersistenceMode.Live};
			Array.Sort(modes);

			Assert.That(modes, Is.EqualTo(new PersistenceMode[] { PersistenceMode.Live, PersistenceMode.Archive }));
		}

		[Test]
		public void Should_determine_enumeration_never_equals_null()
		{
			Assert.That(PersistenceMode.Live, Is.Not.EqualTo(null));
		}

		[Test]
		public void Enumerations_of_different_types_are_not_equal()
		{
			Assert.That(PersistenceMode.Live, Is.Not.EqualTo(TestEnumeration.Red));
		}

		[Test]
		public void Should_return_all_enumerated_values()
		{
			IEnumerable<TestEnumeration> values = Enumeration.GetAll<TestEnumeration>();

			EnumerableAssert.That(values, Has.Count(2));
			EnumerableAssert.Contains(values, TestEnumeration.Red);
			EnumerableAssert.Contains(values, TestEnumeration.Blue);
		}

		[Test]
		public void Should_correctly_return_hash_code_of_integer_value()
		{
			Assert.That(TestEnumeration.Red.GetHashCode(), Is.EqualTo(TestEnumeration.Red.Value.GetHashCode()));
		}

		[Test]
		public void Should_return_vnumerated_value_by_value()
		{
			TestEnumeration value = Enumeration.FromValue<TestEnumeration>(2);

			Assert.AreSame(TestEnumeration.Blue, value);
			Assert.AreNotSame(TestEnumeration.Red, value);
		}

		[Test]
		public void Should_return_enumerated_value_by_display_name()
		{
			TestEnumeration value = Enumeration.FromDisplayName<TestEnumeration>("Red");

			Assert.AreNotSame(TestEnumeration.Blue, value);
			Assert.AreSame(TestEnumeration.Red, value);
		}

		[Test]
		public void Should_correctly_compare_two_enumerated_values()
		{
			Assert.AreEqual(TestEnumeration.Red, TestEnumeration.Red);
			Assert.AreEqual(new TestEnumeration(1, "Red"), TestEnumeration.Red);
			Assert.AreNotEqual(TestEnumeration.Blue, TestEnumeration.Red);
			Assert.AreNotEqual(new TestEnumeration(2, "Red"), TestEnumeration.Red);
		}

		class TestEnumeration : Enumeration
		{
			public static readonly TestEnumeration Red = new TestEnumeration(1, "Red");
			public static readonly TestEnumeration Blue = new TestEnumeration(2, "Blue");

			public TestEnumeration()
			{
			}

			public TestEnumeration(int value, string displayName) : base(value, displayName)
			{
			}
		}
	}
}