using Tarantino.Deployer.Core.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Deployer.Services;
using Tarantino.Deployer.Services.Impl;

namespace Tarantino.UnitTests.Deployer.UI.Services
{
	[TestFixture]
	public class DeploymentSelectionValidatorTester
	{
		[Test]
		public void Correctly_determines_null_deployment_is_invalid()
		{
			IDeploymentSelectionValidator validator = new DeploymentSelectionValidator();
			
			Assert.That(validator.IsValid("845", null), Is.False);
		}

		[Test]
		public void Correctly_determines_existing_deployment_with_empty_revision_number_is_invalid()
		{
			IDeploymentSelectionValidator validator = new DeploymentSelectionValidator();
			
			Assert.That(validator.IsValid(string.Empty, new Deployment()), Is.False);
		}

		[Test]
		public void Correctly_determines_existing_deployment_with_revision_number_is_valid()
		{
			IDeploymentSelectionValidator validator = new DeploymentSelectionValidator();

			Assert.That(validator.IsValid("845", new Deployment()));
		}

		[Test]
		public void Correctly_determines_null_deployment_with_empty_revision_number_is_invalid()
		{
			IDeploymentSelectionValidator validator = new DeploymentSelectionValidator();

			Assert.That(validator.IsValid(string.Empty, null), Is.False);
		}
	}
}