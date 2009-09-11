using System;
using System.IO;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.DatabaseManager.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if(args.Length==4)
            {
                RequestedDatabaseAction Action = (RequestedDatabaseAction) Enum.Parse(typeof(RequestedDatabaseAction), args[0]);
                string Server = args[1];
                string Database = args[2];
                string ScriptDirectory = args[3];

                var deployer = new ConsoleDatabaseDeployer();
                if(deployer.UpdateDatabase(Server, Database, ScriptDirectory, Action))
                {
                    return; 
                }                                
            }
            else
            {
                InvalidArguments();
            }
            Environment.ExitCode = 1;
        }

        private static void InvalidArguments()
        {
            System.Console.WriteLine("Invalid Arguments");
            System.Console.WriteLine( Path.GetFileName(typeof(Program).Assembly.Location) + @" Action(Create|Update|Rebuild) .\SqlExpress DatabaseName  .\DatabaseScripts\ ");
        }
    }
}