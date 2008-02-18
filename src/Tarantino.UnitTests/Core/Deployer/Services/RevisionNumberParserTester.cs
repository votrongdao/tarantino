using System;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Core.Deployer.Services
{
	[TestFixture]
	public class RevisionNumberParserTester
	{
		[Test]
		public void Correctly_parses_revision_number_using_space_as_delimiter()
		{
			IRevisionNumberParser revisionNumberParser = new RevisionNumberParser();
			int revisionNumber = revisionNumberParser.Parse("some text Working Revision Number: 1134 more text");

			Assert.That(revisionNumber, Is.EqualTo(revisionNumber));
		}

		[Test]
		public void Correctly_parses_revision_number_using_hard_return_as_delimiter()
		{
			IRevisionNumberParser revisionNumberParser = new RevisionNumberParser();
			int revisionNumber = revisionNumberParser.Parse("some text Working Revision Number: 1134\n more text");

			Assert.That(revisionNumber, Is.EqualTo(revisionNumber));
		}

		[Test, ExpectedException(typeof(ApplicationException), ExpectedMessage = "The term 'Working Revision Number:' was not found in the build output.  Could not determine the revision number or record deployment occurence!")]
		public void Handles_scenario_where_revision_number_not_included_in_build_output()
		{
			IRevisionNumberParser revisionNumberParser = new RevisionNumberParser();
			int revisionNumber = revisionNumberParser.Parse("some text");

			Assert.That(revisionNumber, Is.EqualTo(revisionNumber));
		}
	}
}