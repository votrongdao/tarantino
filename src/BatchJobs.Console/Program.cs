using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using BatchJobs.Core;
using BatchJobs.UnitTests;

namespace BatchJobs.Console
{
    public class Program
    {
        public const string JobagentfactorytypeKey = "JobAgentFactoryType";

        public Func<string> GetFactoryTypeName = () => ConfigurationManager.AppSettings[JobagentfactorytypeKey];

        private static void Main(string[] args)
        {
            try
            {
                new Program().Run(args);
            }
            catch (Exception e)
            {                
                System.Console.WriteLine(e);
            }
            
        }

        public virtual void Run(string[] args)
        {
            IJobAgent jobAgent = Factory().Create(args[0]);
            jobAgent.Execute();
        }

        public virtual IJobAgentFactory Factory()
        {
            string typename = GetFactoryTypeName();
            string assemblyname = typename.Split(',')[1].Trim();
            typename = typename.Split(',')[0].Trim();

            Assembly a = null;
            try
            {
                a = Assembly.Load(assemblyname);
            }
            catch (FileNotFoundException e)
            {
                System.Console.WriteLine(e.Message);
            }
            Type classType = a.GetType(typename);
            return (IJobAgentFactory) Activator.CreateInstance(classType);
        }
    }
    public class DebugerJobAgentFactory:IJobAgentFactory
    {
        public IJobAgent Create(string name)
        {
            System.Console.WriteLine(name);
            return new DebugerJobAgent();
        }
    }

    public class DebugerJobAgent : IJobAgent
    {
        public void Execute()
        {
            System.Console.WriteLine("Executing");
        }
    }
}