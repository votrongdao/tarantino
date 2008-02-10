using System.Data;
using Tarantino.Commons.Core.Model.Enumerations;
using Tarantino.Commons.Infrastructure.DataAccess.ORMapper;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Commons.Infrastructure.DataAccess.ORMapper
{
	[TestFixture]
	public class EnumerationTypeTester
	{
		[Test]
		public void CorrectlyShowsThatTwoNullValuesAreEqual()
		{
			EnumerationType<PersistenceMode> type = new EnumerationType<PersistenceMode>();

			Assert.That(type.Equals(null, null), Is.True);
		}

		[Test]
		public void CorrectlyShowsThatTwoNullValuesAreNotEqual()
		{
			EnumerationType<PersistenceMode> type = new EnumerationType<PersistenceMode>();

			Assert.That(type.Equals(null, 10), Is.False);
		}

		[Test]
		public void Should_Get_Enumerator_From_DataRader_by_Name()
		{
			MockRepository mocks = new MockRepository();
			IDataReader dataReader = mocks.CreateMock<IDataReader>();

			Expect.Call(dataReader.GetOrdinal(PersistenceMode.Live.DisplayName)).Return(PersistenceMode.Live.Value);
			Expect.Call(dataReader[PersistenceMode.Live.Value]).Return(PersistenceMode.Live.Value);

			mocks.ReplayAll();

			EnumerationType<PersistenceMode> type = new EnumerationType<PersistenceMode>();
			PersistenceMode mode = (PersistenceMode)type.Get(dataReader, PersistenceMode.Live.DisplayName);
			Assert.That(mode, Is.SameAs(PersistenceMode.Live));
			mocks.VerifyAll();
		}

		[Test]
		public void Name_should_return_ListItem()
		{
			EnumerationType<PersistenceMode> type = new EnumerationType<PersistenceMode>();
			Assert.That(type.Name, Is.EqualTo("Enumeration"));
		}

		[Test]
		public void FromStringValue_should_return_an_integer()
		{
			EnumerationType<PersistenceMode> type = new EnumerationType<PersistenceMode>();
			Assert.That(type.FromStringValue("10"), Is.EqualTo(10));
		}

		[Test]
		public void ObjectToSQLString_should_return_the_ToString_of_the_object()
		{
			EnumerationType<PersistenceMode> type = new EnumerationType<PersistenceMode>();
			Assert.That(type.ObjectToSQLString(PersistenceMode.Live), Is.EqualTo(PersistenceMode.Live.ToString()));
		}

	}
}