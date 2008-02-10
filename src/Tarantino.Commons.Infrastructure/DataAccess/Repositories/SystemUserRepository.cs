using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Model;
using Tarantino.Commons.Core.Model.Repositories;
using StructureMap;

namespace Tarantino.Commons.Infrastructure.DataAccess.Repositories
{
	[Pluggable(ServiceKeys.Default)]
	public class SystemUserRepository : ISystemUserRepository
	{
		protected readonly IPersistentObjectRepository _repository;

		public SystemUserRepository(IPersistentObjectRepository repository)
		{
			_repository = repository;
		}

		public bool IsValidLogin(string emailAddress, string password)
		{
			SystemUser getMatchingUser = getSystemUser(emailAddress, password);
			
			return getMatchingUser != null;
		}

		public SystemUser GetByEmailAddress(string emailAddress)
		{
			SystemUser matchingUser = getSystemUser(emailAddress, null);
			return matchingUser;
		}

		private SystemUser getSystemUser(string emailAddress, string password)
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(SystemUser.EMAIL_ADDRESS, emailAddress));

			if (password != null)
			{
				criteria.AddCriterion(new Criterion(SystemUser.PASSWORD, password));
			}

			SystemUser user = _repository.FindFirst<SystemUser>(criteria);

			return user;
		}
	}
}