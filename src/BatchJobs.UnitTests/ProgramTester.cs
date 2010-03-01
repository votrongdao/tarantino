using System;
using System.Collections.Generic;
using BatchJobs.Console;
using BatchJobs.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace BatchJobs.UnitTests
{
    [TestFixture]
    public class ProgramTester
    {
        [Test]
        public void Should_create_a_factory()
        {
            var program = new Program();
            program.GetFactoryTypeName = () => typeof (FactoryStub).FullName + "," + GetType().Assembly.FullName;
            IJobAgentFactory factory = program.Factory();
            Assert.IsAssignableFrom(typeof (FactoryStub), factory);
        }

        [Test]
        public void Should_locate_a_factory_and_execute_a_job()
        {
            var agent = new StubJob();
            FactoryStub.JobAgent = agent;
            var program = new Program();
            program.GetFactoryTypeName = () => typeof (FactoryStub).FullName + "," + GetType().Assembly.FullName;
            program.Run(new[] {"foo"});
            Assert.That(FactoryStub.Name, Is.EqualTo("foo"));
            Assert.That(agent.Executed, Is.True);
        }

    	[Test]
    	public void Should_create_a_logging_factory()
    	{
			var program = new Program();
			program.GetFactoryTypeName = () => typeof(FactoryLoggingStub).FullName + "," + GetType().Assembly.FullName;
			IJobAgentFactory factory = program.Factory();
			Assert.AreEqual("BatchJobs.UnitTests.FactoryLoggingStub", factory.GetType().ToString());

    	}
    }

    public class StubJob : IJobAgent
    {
        public bool Executed { get; set; }


        public void Execute()
        {
            Executed = true;
        }
    }

	public class StubJobWithContructorParam : IJobAgent
	{

		public StubJobWithContructorParam(ILogger logger)
		{
		}

		public void Execute()
		{

		}
	}

    public class FactoryStub : IJobAgentFactory
    {
        public FactoryStub()
        {
            if(JobAgent==null)
            {
                JobAgent = new StubJob();
            }
        }

        public static IJobAgent JobAgent { get; set; }

        public static string Name { get; set; }


        public IJobAgent Create(string name)
        {
            System.Console.WriteLine(name);
            Name = name;
            return JobAgent;
        }

        public IEnumerable<string> GetInstanceNames()
        {
            return new string[0];
        }
    }

	public class FactoryLoggingStub : IJobAgentFactory
	{
		private readonly ILogger _logger;

		public FactoryLoggingStub(ILogger logger)
		{
			_logger = logger;
		}

		public FactoryLoggingStub()
		{
			if (JobAgent == null)
			{
				JobAgent = new StubJobWithContructorParam(_logger);
			}
		}

		public static IJobAgent JobAgent { get; set; }

		public static string Name { get; set; }


		public IJobAgent Create(string name)
		{
			System.Console.WriteLine(name);
			Name = name;
			return JobAgent;
		}

		public IEnumerable<string> GetInstanceNames()
		{
			return new string[0];
		}
	}
}