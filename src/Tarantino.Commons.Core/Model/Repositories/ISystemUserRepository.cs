namespace Tarantino.Commons.Core.Model.Repositories
{
	public interface ISystemUserRepository
	{
		bool IsValidLogin(string emailAddress, string password);
		ISystemUser GetByEmailAddress(string emailAddress);
	}
}