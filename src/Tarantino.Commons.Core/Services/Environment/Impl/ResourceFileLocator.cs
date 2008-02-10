using System;
using System.IO;
using System.Reflection;
using System.Text;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ResourceFileLocator : IResourceFileLocator
	{
		public string ReadTextFile(string resourceName)
		{
			using (Stream stream = getStream(resourceName))
			{
				using (StreamReader reader = new StreamReader(stream, Encoding.Default))
				{
					string contents = reader.ReadToEnd();
					return contents;
				}
			}
		}

		public byte[] ReadBinaryFile(string resourceName)
		{
			using (Stream stream = getStream(resourceName))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					byte[] contents = reader.ReadBytes((int)stream.Length);
					return contents;
				}
			}
		}

		public bool FileExists(string resourceName)
		{
			Stream stream = constructStream(resourceName);
			bool fileExists = stream != null;
			return fileExists;
		}

		private Stream getStream(string resourceName)
		{
			Stream stream = constructStream(resourceName);

			if (stream == null)
			{
				string template = "Resource file not found: {0}. Make sure the Build Action for the file is 'Embedded Resource'.";
				throw new ApplicationException(string.Format(template, resourceName));
			}

			return stream;
		}

		private Stream constructStream(string resourceName)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
		}
	}
}