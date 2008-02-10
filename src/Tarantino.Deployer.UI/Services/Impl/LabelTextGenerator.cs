using System;
using System.Text;
using Tarantino.Deployer.Core.Model;
using Tarantino.Commons.Core;
using StructureMap;
using Environment=Tarantino.Deployer.Core.Services.Configuration.Impl.Environment;

namespace Tarantino.Deployer.UI.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class LabelTextGenerator : ILabelTextGenerator
	{
		private readonly IDeploymentSelectionValidator _validator;

		public LabelTextGenerator(IDeploymentSelectionValidator validator)
		{
			_validator = validator;
		}

		public string GetDeploymentText(Environment environment, string revisionNumberText, Deployment deployment)
		{
			return getText(environment, revisionNumberText, deployment, Action.Deploy);
		}

		public string GetCertificationText(string revisionNumberText, Deployment deployment)
		{
			return getText(null, revisionNumberText, deployment, Action.Certify);
		}

		private string getText(Environment environment, string revisionNumberText, Deployment deployment, Action action)
		{
			StringBuilder text = new StringBuilder();

			if (_validator.IsValid(revisionNumberText, deployment))
			{
				bool isDeployment = action == Action.Deploy;
				string username = isDeployment ? deployment.DeployedBy : deployment.CertifiedBy;
				DateTime date = isDeployment ? deployment.DeployedOn : deployment.CertifiedOn.Value;

				if (environment != null)
				{
					text.AppendFormat("{0} on ", environment.Predecessor);
				}

				text.AppendFormat("{0} by {1}", date.ToString("g"), username);
			}

			return text.ToString();
		}

		enum Action
		{
			Deploy,
			Certify
		}
	}
}