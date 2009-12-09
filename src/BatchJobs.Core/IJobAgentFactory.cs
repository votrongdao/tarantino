using BatchJobs.Core;

namespace BatchJobs.UnitTests
{
    public interface IJobAgentFactory
    {
        IJobAgent Create(string name);
    }
}