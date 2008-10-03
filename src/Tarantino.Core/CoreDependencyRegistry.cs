using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;

namespace Tarantino.Core
{
	public class CoreDependencyRegistry : Registry
	{
		protected override void configure()
		{
			Scan(x =>
			     	{
			     		x.AssemblyContainingType(GetType());
			     		x.With<DefaultConventionScanner>();
			     	});

			BuildInstancesOf<IEncryptionEngine>().TheDefaultIsConcreteType<AesEncryptionEngine>();
			BuildInstancesOf<IHashAlgorithm>().TheDefaultIsConcreteType<SHA512HashAlgorithm>();
		}
	}
}