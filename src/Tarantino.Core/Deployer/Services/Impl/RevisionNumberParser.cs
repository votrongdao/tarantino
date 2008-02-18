using System;
using Tarantino.Commons.Core;
using StructureMap;

namespace Tarantino.Deployer.Core.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class RevisionNumberParser : IRevisionNumberParser
	{
		private const string _searchString = "Working Revision Number: ";

		public int Parse(string output)
		{
			int position = output.IndexOf(_searchString) + _searchString.Length;

			if (position < _searchString.Length)
			{
				throw new ApplicationException("The term 'Working Revision Number:' was not found in the build output.  Could not determine the revision number or record deployment occurence!");
			}

			string revisionNumberString = string.Empty;
			while (getChar(output, position) != " " && getChar(output, position) != "\n")
			{
				revisionNumberString += output.Substring(position, 1);
				position++;
			}

			int revisionNumber = int.Parse(revisionNumberString);

			return revisionNumber;
		}

		private string getChar(string output, int position)
		{
			return output.Substring(position, 1);
		}
	}
}