using System;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Commons.Core.Model;
using NUnit.Framework;
using Tarantino.UnitTests.Commons.Core.Model;

namespace Tarantino.UnitTests.Commons.Core.Model
{
	[TestFixture]
	public class SystemUserTester : PersistentObjectTester
	{
		[Test]
		public void Property_Accessors_Work()
		{
			SystemUser user = new SystemUser();

			Assert.AreEqual(null, user.EmailAddress);
			user.EmailAddress = "EmailAddress";
			Assert.AreEqual("EmailAddress", user.EmailAddress);

			Assert.AreEqual(null, user.Password);
			user.Password = "Password";
			Assert.AreEqual("Password", user.Password);

			Assert.AreEqual(null, user.CreatedDate);
			user.CreatedDate = new DateTime(2007, 4, 15);
			Assert.AreEqual(new DateTime(2007, 4, 15), user.CreatedDate);
		}

		[Test]
		public void Constructor_works()
		{
			SystemUser user = new SystemUser("email", "pass", new DateTime(2007, 4, 15));

			Assert.That(user.EmailAddress, Is.EqualTo("email"));
			Assert.That(user.Password, Is.EqualTo("pass"));
			Assert.That(user.CreatedDate, Is.EqualTo(new DateTime(2007, 4, 15)));
		}

		protected override PersistentObject CreatePersisentObject()
		{
			return new SystemUser();
		}
	}
}