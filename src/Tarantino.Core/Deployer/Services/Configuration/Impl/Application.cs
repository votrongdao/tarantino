using System.Configuration;
using Tarantino.Commons.Core.Services.Configuration.Impl;

namespace Tarantino.Deployer.Core.Services.Configuration.Impl
{
	public sealed class Application : NamedElement
	{
		[ConfigurationProperty("Url", IsRequired = true)]
		public string Url
		{
			get { return (string)this["Url"]; }
		}

		[ConfigurationProperty("ZipFile", IsRequired = true)]
		public string ZipFile
		{
			get { return (string)this["ZipFile"]; }
		}

		[ConfigurationProperty("Username", IsRequired = true)]
		public string Username
		{
			get { return (string)this["Username"]; }
		}

		[ConfigurationProperty("Password", IsRequired = true)]
		public string Password
		{
			get { return (string)this["Password"]; }
		}

		[ConfigurationProperty("Environments", IsDefaultCollection = false)]
		public ElementCollection<Environment> Environments
		{
			get { return (ElementCollection<Environment>)base["Environments"]; }
		}

		public override string GetElementName()
		{
			return "Application";
		}
	}
}