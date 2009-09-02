using System;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.DatabaseManager.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //"DeployDatabase.exe Action(Create|Update) .\SqlExpress CEMS_DEV  .\Database\ [[username] [password]]"
            if(args.Length==4)
            {
                RequestedDatabaseAction Action = (RequestedDatabaseAction) Enum.Parse(typeof(RequestedDatabaseAction), args[0]);
                string Server = args[1];
                string Database = args[2];
                string ScriptDirectory = args[3];

                if (Action != RequestedDatabaseAction.Create && Action != RequestedDatabaseAction.Update)
                {
                    System.Console.WriteLine("The Action must be Create or Update");
                    InvalidArguments();
                    return;
                }

                var deployer = new ConsoleDatabaseDeployer();
                deployer.UpdateDatabase(Server, Database, ScriptDirectory, Action);
                
            }
            else if(args.Length==6)
            {
                throw  new NotImplementedException("This should be considnered not secure");
            }
            else
            {
                InvalidArguments();
            }
        }

        private static void InvalidArguments()
        {
            System.Console.WriteLine("Invalid Arguments");
            System.Console.WriteLine(
                @"DeployDatabase.exe Action(Create|Update) .\SqlExpress DatabaseName  .\DatabaseScripts\ [[username] [password]]");
        }
    }
}