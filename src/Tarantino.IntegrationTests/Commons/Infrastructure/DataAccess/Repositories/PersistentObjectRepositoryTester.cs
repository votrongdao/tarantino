using System;
using System.Collections.Generic;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Model;
using Tarantino.Commons.Core.Model.Enumerations;
using Tarantino.Commons.Core.Model.Repositories;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.IntegrationTests.Commons.Infrastructure.DataAccess;

namespace Tarantino.IntegrationTests.Commons.Infrastructure
{
	[TestFixture]
	public class PersistentObjectRepositoryTester : DatabaseTesterBase
	{
		private SystemUser _systemUser1;
		private SystemUser _systemUser2;

		protected override void SetupDatabase()
		{
			_systemUser1 = new SystemUser();
			_systemUser2 = new SystemUser();

			_systemUser1.CreatedDate = new DateTime(2007, 5, 15);

			_systemUser2.EmailAddress = "khurwitz@hotmail.com";
			_systemUser2.CreatedDate = new DateTime(2007, 4, 15);

			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();
			repository.Save(_systemUser1);
			repository.Save(_systemUser2);
		}

		[Test]
		public void Can_find_user_using_criteria()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();

			CriterionSet set = new CriterionSet();
			set.AddCriterion(new Criterion(SystemUser.ID, _systemUser2.Id));

			Assert.That(repository.FindAll<SystemUser>(set), Is.EqualTo(new SystemUser[]{_systemUser2}));
			Assert.That(repository.FindFirst<SystemUser>(set), Is.EqualTo(_systemUser2));
		}

		[Test]
		public void Can_find_user_using_null_string_criteria()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();

			CriterionSet set = new CriterionSet();
			set.AddCriterion(new Criterion(SystemUser.EMAIL_ADDRESS, null));

			Assert.That(repository.FindAll<SystemUser>(set), Is.EquivalentTo(
				new SystemUser[]{ _systemUser1 }));
		}

		[Test]
		public void Can_find_user_using_not_null_string_criteria()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();

			CriterionSet set = new CriterionSet();
			set.AddCriterion(new Criterion(SystemUser.EMAIL_ADDRESS, null, ComparisonOperator.NotEqual));

			Assert.That(repository.FindAll<SystemUser>(set), Is.EquivalentTo(
				new SystemUser[]{ _systemUser2 }));
		}

		[Test]
		public void Can_find_users_in_order_of_create_date_descending()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();

			CriterionSet set = new CriterionSet();
			set.OrderBy = SystemUser.CREATED_DATE;
			set.SortOrder = SortOrder.Ascending;

			Assert.That(repository.FindAll<SystemUser>(set), Is.EqualTo(
				new SystemUser[]{ _systemUser2, _systemUser1 }));
		}

		[Test]
		public void Can_find_user_using_not_equal_string_criteria()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();

			CriterionSet set = new CriterionSet();
			set.AddCriterion(new Criterion(SystemUser.EMAIL_ADDRESS, "khurwitz@hotmail.com", ComparisonOperator.NotEqual));

			Assert.That(repository.FindAll<SystemUser>(set), Is.EquivalentTo(
				new SystemUser[0]));
		}

		[Test]
		public void Can_find_all_users_by_not_using_criteria()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();

			CriterionSet set = new CriterionSet();

			IEnumerable<SystemUser> users = repository.FindAll<SystemUser>(set);
			Assert.That(users, Is.EquivalentTo(new SystemUser[]{_systemUser1, _systemUser2}));
		}

		[Test]
		public void Can_get_all_system_users()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();
			IEnumerable<SystemUser> systemUsers = repository.GetAll<SystemUser>();
			EnumerableAssert.That(systemUsers, Is.EquivalentTo(new SystemUser[]{_systemUser1, _systemUser2}));
		}

		[Test]
		public void Can_get_single_system_user()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();
			SystemUser systemUser = repository.GetById<SystemUser>(_systemUser2.Id);
			Assert.That(systemUser, Is.EqualTo(_systemUser2));
		}

		[Test]
		public void Can_delete_single_system_user()
		{
			IPersistentObjectRepository repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();
			repository.Delete(_systemUser1);

			IEnumerable<SystemUser> systemUsers = repository.GetAll<SystemUser>();
			EnumerableAssert.That(systemUsers, Is.EquivalentTo(new SystemUser[] { _systemUser2 }));
		}

		protected override Type GetEntityType()
		{
			return typeof(SystemUser);
		}
	}
}