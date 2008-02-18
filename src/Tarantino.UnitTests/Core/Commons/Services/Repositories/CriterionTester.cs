using Tarantino.Core.Commons.Model.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.UnitTests.Core.Commons.Services.Repositories
{
	[TestFixture]
	public class CriterionTester
	{
		[Test]
		public void Property_accessors_work()
		{
			Criterion criterion = new Criterion();

			Assert.AreEqual(null, criterion.Attribute);
			criterion.Attribute = "Attribute";
			Assert.AreEqual("Attribute", criterion.Attribute);

			Assert.AreEqual(null, criterion.Value);
			int value = 5;
			criterion.Value = value;
			Assert.AreEqual(value, criterion.Value);

			Assert.AreEqual(null, criterion.Operator);
			criterion.Operator = ComparisonOperator.GreaterThan;
			Assert.AreSame(ComparisonOperator.GreaterThan, criterion.Operator);
		}

		[Test]
		public void Constructor_works()
		{
			Criterion criterion1 = new Criterion("attribute", "value", ComparisonOperator.LessThan);

			Assert.That(criterion1.Attribute, Is.EqualTo("attribute"));
			Assert.That(criterion1.Value, Is.EqualTo("value"));
			Assert.That(criterion1.Operator, Is.SameAs(ComparisonOperator.LessThan));

			Criterion criterion2 = new Criterion("attribute", "value");

			Assert.That(criterion2.Attribute, Is.EqualTo("attribute"));
			Assert.That(criterion2.Value, Is.EqualTo("value"));
			Assert.That(criterion2.Operator, Is.SameAs(ComparisonOperator.Equal));
		}
	}
}