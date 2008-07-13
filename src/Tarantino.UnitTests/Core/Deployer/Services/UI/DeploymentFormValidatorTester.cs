using Tarantino.Core.Deployer.Services.Configuration.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Deployer.Services.UI;
using Tarantino.Core.Deployer.Services.UI.Impl;

namespace Tarantino.UnitTests.Core.Deployer.Services.UI
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