using System;
using Tarantino.DatabaseManager.Services;
using Tarantino.DatabaseManager.Services.Impl;
using NUnit.Framework;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class TokenReplacerTester
	{
		[Test]
		public void CorrectlyReplacesToken()
		{
			string script = "X ||Token|| X";

			ITokenReplacer replacer = new TokenReplacer();
			replacer.Text = script;
			replacer.Replace("Token", "TokenValue");
			Assert.AreEqual("X TokenValue X", replacer.Text);
		}

		[Test, ExpectedException(typeof(ApplicationException), ExpectedMessage = "The Text property must be set before tokens may be replaced")]
		public void ThrowsExceptionIfTextNotSetFirst()
		{
			ITokenReplacer replacer = new TokenReplacer();
			replacer.Replace("Token", "TokenValue");
		}
	}
}