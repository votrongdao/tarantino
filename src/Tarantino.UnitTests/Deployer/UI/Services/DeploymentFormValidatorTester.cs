using Tarantino.Deployer.Core.Services.Configuration.Impl;
using Tarantino.Deployer.UI.Services;
using Tarantino.Deployer.UI.Services.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Deployer.UI.Services
{
	[TestFixture]
	public class DeploymentFormValidatorTester
	{
		[Test]
		public void Determines_that_environment_with_predecessor_must_have_revision()
		{
			IDeploymentFormValidator validator = new DeploymentFormValidator();

			Environment environment = new Environment();
			environment.Predecessor = "QA";

			bool isValid = validator.IsValid(environment, string.Empty);

			Assert.That(isValid, Is.False);
		}

		[Test]
		public void Determines_that_environment_without_predecessor_is_valid()
		{
			IDeploymentFormValidator validator = new DeploymentFormValidator();

			Environment environment = new Environment();

			Assert.That(validator.IsValid(environment, string.Empty), Is.True);
			Assert.That(validator.IsValid(environment, "845"), Is.True);
		}

		[Test]
		public void Determines_that_environment_with_predecessor_and_revision_is_valid()
		{
			IDeploymentFormValidator validator = new DeploymentFormValidator();

			Environment environment = new Environment();
			environment.Predecessor = "QA";

			Assert.That(validator.IsValid(environment, "845"), Is.True);
		}
	}
}