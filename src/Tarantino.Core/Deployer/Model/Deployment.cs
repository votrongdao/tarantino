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

		private string _application;
		private string _environment;
		private int _revision;
		private DateTime _deployedOn;
		private string _deployedBy;
		private DateTime? _certifiedOn;
		private string _certifiedBy;
		private string _output;
		private DeploymentResult _result;

		public string Application
		{
			get { return _application; }
			set { _application = value; }
		}

		public string Environment
		{
			get { return _environment; }
			set { _environment = value; }
		}

		public int Revision
		{
			get { return _revision; }
			set { _revision = value; }
		}

		public DateTime DeployedOn
		{
			get { return _deployedOn; }
			set { _deployedOn = value; }
		}

		public DateTime? CertifiedOn
		{
			get { return _certifiedOn; }
			set { _certifiedOn = value; }
		}

		public string DeployedBy
		{
			get { return _deployedBy; }
			set { _deployedBy = value; }
		}

		public string CertifiedBy
		{
			get { return _certifiedBy; }
			set { _certifiedBy = value; }
		}

		public string Output
		{
			get { return _output; }
			set { _output = value; }
		}

		public DeploymentResult Result
		{
			get { return _result; }
			set { _result = value; }
		}

		public override string ToString()
		{
			return _revision.ToString();
		}
	}
}