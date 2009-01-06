using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Core.Deployer.Services.Configuration;
using Tarantino.Core.Deployer.Services.Configuration.Impl;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using NUnit.Framework;
using Tarantino.Infrastructure;

namespace Tarantino.IntegrationTests.Core.Deployer.Services.Configuration
{
	[TestFixture]
	public class ApplicationRepositoryTester
	{
		[Test]
		public void Reads_application_collection_from_configuration_file()
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();

			var repository = ObjectFactory.GetInstance<IApplicationRepository>();
			ElementCollection<Application> applications = repository.GetAll();

			Assert.That(applications, Is.Not.Null);

			Assert.That(applications.Count, Is.EqualTo(2));

			Assert.That(applications[0].Name, Is.EqualTo("SampleApp1"));
			Assert.That(applications[0].Url, Is.EqualTo("http://svn.com/SampleApp1"));
			Assert.That(applications[0].ZipFile, Is.EqualTo("SampleApp1File"));
			Assert.That(applications[0].Username, Is.EqualTo("user1"));
			Assert.That(applications[0].Password, Is.EqualTo("password1"));
			Assert.That(applications[0].Environments.Count, Is.EqualTo(2));
			Assert.That(applications[0].Environments[0].Name, Is.EqualTo("Development"));
			Assert.That(applications[0].Environments[0].ToString(), Is.EqualTo("Development"));
			Assert.That(applications[0].Environments[0].Predecessor, Is.Empty);
			Assert.That(applications[0].Environments[1].Name, Is.EqualTo("Production"));
			Assert.That(applications[0].Environments[1].ToString(), Is.EqualTo("Production"));
			Assert.That(applications[0].Environments[1].Predecessor, Is.EqualTo("Development"));

			Assert.That(applications[1].Name, Is.EqualTo("SampleApp2"));
			Assert.That(applications[1].Url, Is.EqualTo("http://svn.com/SampleApp2"));
			Assert.That(applications[1].ZipFile, Is.EqualTo("SampleApp2File"));
			Assert.That(applications[1].Username, Is.EqualTo("user2"));
			Assert.That(applications[1].Password, Is.EqualTo("password2"));
			Assert.That(applications[1].Environments.Count, Is.EqualTo(2));
			Assert.That(applications[1].Environments[0].Name, Is.EqualTo("Development"));
			Assert.That(applications[1].Environments[0].ToString(), Is.EqualTo("Development"));
			Assert.That(applications[1].Environments[0].Predecessor, Is.Empty);
			Assert.That(applications[1].Environments[1].Name, Is.EqualTo("Production"));
			Assert.That(applications[1].Environments[1].ToString(), Is.EqualTo("Production"));
			Assert.That(applications[1].Environments[1].Predecessor, Is.EqualTo("Development"));
		}

		[Test]
		public void Reads_a_single_application_from_configuration_file()
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();

			var repository = ObjectFactory.GetInstance<IApplicationRepository>();
			Application application = repository.GetByName("SampleApp2");

			Assert.That(application.Name, Is.EqualTo("SampleApp2"));
			Assert.That(application.Url, Is.EqualTo("http://svn.com/SampleApp2"));
			Assert.That(application.ZipFile, Is.EqualTo("SampleApp2File"));
			Assert.That(application.Username, Is.EqualTo("user2"));
			Assert.That(application.Password, Is.EqualTo("password2"));
			Assert.That(application.Environments.Count, Is.EqualTo(2));
			Assert.That(application.Environments[0].Name, Is.EqualTo("Development"));
			Assert.That(application.Environments[0].ToString(), Is.EqualTo("Development"));
			Assert.That(application.Environments[0].Predecessor, Is.Empty);
			Assert.That(application.Environments[1].Name, Is.EqualTo("Production"));
			Assert.That(application.Environments[1].ToString(), Is.EqualTo("Production"));
			Assert.That(application.Environments[1].Predecessor, Is.EqualTo("Development"));
		}

		[Test]
		public void Returns_undefined_application_when_application_not_in_the_configuration_file()
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();

			var repository = ObjectFactory.GetInstance<IApplicationRepository>();
			Application application = repository.GetByName("SampleApp3");

			Assert.That(application, Is.Null);
		}
	}
}