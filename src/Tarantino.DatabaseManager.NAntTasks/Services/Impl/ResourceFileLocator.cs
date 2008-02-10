using System.IO;
using System.Reflection;

namespace Tarantino.DatabaseManager.NAntTasks.Services.Impl
{
	public class ResourceFileLocator : IResourceFileLocator
	{
		public string ReadTextFile(string resourceName)
		{
			string text;
			
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					text = reader.ReadToEnd();
				}
			}
			return text;
		}
	}
}