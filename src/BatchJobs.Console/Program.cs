using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using BatchJobs.Core;

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
                System.Console.WriteLine(e.Message);
                Environment.ExitCode = 100;                    
            }            
        }

        public virtual void Run(string[] args)
        {
            if(args.Length==0)
            {
                System.Console.WriteLine("One of the following instance names must be specified:");
                foreach(var name in Factory().GetInstanceNames())
                {
                    System.Console.WriteLine(name);
                }
            }
            else
            {
                IJobAgent jobAgent = Factory().Create(args[0]);
                jobAgent.Execute();                
            }
        }

        public virtual IJobAgentFactory Factory()
        {
            string typename = GetFactoryTypeName();
            string assemblyname;

            try
            {
                assemblyname = typename.Split(',')[1].Trim();
                typename = typename.Split(',')[0].Trim();
            }
            catch(Exception)
            {
                throw new InvalidOperationException("Could not parse the typename from the application configuration.");
            }            

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

        public IEnumerable<string> GetInstanceNames()
        {
            return new string[]{"Foo","Bar"};            
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