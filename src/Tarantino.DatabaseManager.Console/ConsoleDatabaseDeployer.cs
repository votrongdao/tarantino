using System;
using System.Collections.Generic;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.DatabaseManager.Console
{
    public class ConsoleDatabaseDeployer:ITaskObserver
    {
        IDictionary<string,string> _properties = new Dictionary<string, string>();
        public void Log(string message)
        {
            System.Console.WriteLine(message);
        }

        public void SetVariable(string name, string value)
        {
            if(_properties.ContainsKey(name))
            {
                _properties[name] = value;
            }
            else
            {
                _properties.Add(name, value);    
            }
            
        }

        public void UpdateDatabase(string Server, string Database, string ScriptDirectory, RequestedDatabaseAction Action)
        {
            var manager = new SqlDatabaseManager();
            var settings = new ConnectionSettings(Server, Database, true, null, null);
            var taskAttributes = new TaskAttributes(settings, ScriptDirectory)
                                     {
                                         RequestedDatabaseAction = Action,
                                     };
            try
            {
                manager.Upgrade(taskAttributes, this);

                foreach (var property in _properties)
                {
                    Log(property.Key +": " + property.Value);
                }

            }
            catch (Exception exception)
            {                
                var ex = exception;
                do
                {
                    Log("Failure: " + ex.Message);
                    ex = ex.InnerException;    
                } while (ex!=null);

                //Log(exception.ToString());
                

            }
            
        }
    }
}