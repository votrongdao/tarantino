using System;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Logging
{
	[PluginFamily(ServiceKeys.Default)]
	public interface ILogger
	{
		string SerializeException(Exception e);
		void Debug(object source, object message);
		void Debug(Type source, object message);
		void Info(object source, object message);
		void Info(Type source, object message);
		void Warn(object source, object message);
		void Warn(Type source, object message);
		void Error(object source, object message);
		void Error(Type source, object message);
		void Fatal(object source, object message);
		void Fatal(Type source, object message);
		void Debug(object source, object message, Exception exception);
		void Debug(Type source, object message, Exception exception);
		void Info(object source, object message, Exception exception);
		void Info(Type source, object message, Exception exception);
		void Warn(object source, object message, Exception exception);
		void Warn(Type source, object message, Exception exception);
		void Error(object source, object message, Exception exception);
		void Error(Type source, object message, Exception exception);
		void Fatal(object source, object message, Exception exception);
		void Fatal(Type source, object message, Exception exception);
	}
}