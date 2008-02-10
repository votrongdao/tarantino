using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Commons.Core.Services.Security;
using Tarantino.Commons.Core.Services.Security.Impl;

namespace Tarantino.UnitTests.Commons.Core.Services.Security
{
	[TestFixture]
	public class AesEncryptionEngineTester
	{
		[Test]
		public void Encrypts_string()
		{
			IEncryptionEngine engine = new AesEncryptionEngine();

			string encrypted = engine.Encrypt("hello");

			Assert.That(encrypted, Is.EqualTo("5XsF3/9oUZnkt36yYQczUQ=="));
		}

		[Test]
		public void Decrypts_string()
		{
			IEncryptionEngine engine = new AesEncryptionEngine();

			string decrypted = engine.Decrypt("5XsF3/9oUZnkt36yYQczUQ==");

			Assert.That(decrypted, Is.EqualTo("hello"));
		}
	}
}