using Tarantino.Commons.Core.Model.Repositories;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Security.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class LoginChecker : ILoginChecker
	{
		private readonly ISystemUserRepository _repository;
		private readonly IEncryptionEngine _encryptionEngine;

		public LoginChecker(ISystemUserRepository repository, IEncryptionEngine encryptionEngine)
		{
			_repository = repository;
			_encryptionEngine = encryptionEngine;
		}

		public bool IsValidUser(string emailAddress, string cleartextPassword)
		{
			string encryptedPassword = _encryptionEngine.Encrypt(cleartextPassword);
			bool isValidUser = _repository.IsValidLogin(emailAddress, encryptedPassword);

			return isValidUser;
		}
	}
}