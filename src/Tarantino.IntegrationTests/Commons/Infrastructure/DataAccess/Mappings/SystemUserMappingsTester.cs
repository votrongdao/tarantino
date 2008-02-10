using System;
using Tarantino.Commons.Core.Model;
using NUnit.Framework;

namespace Tarantino.IntegrationTests.Commons.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class SystemUserMappingsTester : DatabaseTesterBase
	{
		[Test]
		public void Can_add_user()
		{
			SystemUser user = new SystemUser();

			user.EmailAddress = "123@abc.com";
			user.Password = "mypass";
			user.CreatedDate = new DateTime(2007, 4, 15);

			SaveAndFlushSessionFor(user);

			LoadFromDatabaseAndAssertMatchFor(user);
		}

		protected override Type GetEntityType()
		{
			return typeof(SystemUser);
		}
	}
}