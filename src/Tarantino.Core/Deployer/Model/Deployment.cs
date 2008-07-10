using System;
using Tarantino.Core.Commons.Model;

namespace Tarantino.Core.Deployer.Model
{
	public class Deployment : PersistentObject
	{
		public const string APPLICATION = "Application";
		public const string ENVIRONMENT = "Environment";
		public const string DEPLOYED_ON = "DeployedOn";
		public const string CERTIFIED_ON = "CertifiedOn";
		public const string RESULT = "Result";

		public virtual string Application { get; set; }
		public virtual string Environment { get; set; }
		public virtual int Revision { get; set; }
		public virtual DateTime DeployedOn { get; set; }
		public virtual DateTime? CertifiedOn { get; set; }
		public virtual string DeployedBy { get; set; }
		public virtual string CertifiedBy { get; set; }
		public virtual string Output { get; set; }
		public virtual DeploymentResult Result { get; set; }

		public override string ToString()
		{
			return Revision.ToString();
		}
	}
}