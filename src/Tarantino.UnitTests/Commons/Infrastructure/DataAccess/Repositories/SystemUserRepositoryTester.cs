using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Model;
using Tarantino.Commons.Core.Model.Repositories;
using Tarantino.Commons.Infrastructure.DataAccess.Repositories;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Commons.Infrastructure.DataAccess.Repositories
{
	[TestFixture]
	public class SystemUserRepositoryTester
	{
		[Test]
		public void Correctly_finds_user_by_email_address()
		{
			SystemUser user = new SystemUser();
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(SystemUser.EMAIL_ADDRESS, "test@test.com"));

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				Expect.Call(objectRepository.FindFirst<SystemUser>(criteria)).Return(user);
			}

			using (mocks.Playback())
			{
				ISystemUserRepository repository = createSystemUserRepository(objectRepository);
				SystemUser foundUser = repository.GetByEmailAddress("test@test.com");

				Assert.That(foundUser, Is.SameAs(user));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_validates_successful_login_by_username_and_password()
		{
			SystemUser user = new SystemUser();
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(SystemUser.EMAIL_ADDRESS, "test@test.com"));
			criteria.AddCriterion(new Criterion(SystemUser.PASSWORD, "pass"));

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				Expect.Call(objectRepository.FindFirst<SystemUser>(criteria)).Return(user);
			}

			using (mocks.Playback())
			{
				ISystemUserRepository repository = new SystemUserRepository(objectRepository);
				bool isValidLogin = repository.IsValidLogin("test@test.com", "pass");

				Assert.That(isValidLogin);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Does_not_validates_successful_login_by_username_and_password()
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(SystemUser.EMAIL_ADDRESS, "test@test.com"));
			criteria.AddCriterion(new Criterion(SystemUser.PASSWORD, "invalid password"));

			MockRepository mocks = new MockRepository();
			IPersistentObjectRepository objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				Expect.Call(objectRepository.FindFirst<SystemUser>(criteria)).Return(null);
			}

			using (mocks.Playback())
			{
				ISystemUserRepository repository = new SystemUserRepository(objectRepository);
				bool isValidLogin = repository.IsValidLogin("test@test.com", "invalid password");

				Assert.That(isValidLogin, Is.False);
			}

			mocks.VerifyAll();
		}

		protected virtual SystemUserRepository createSystemUserRepository(IPersistentObjectRepository objectRepository)
		{
			SystemUserRepository repository = new SystemUserRepository(objectRepository);
			return repository;
		}
	}
}