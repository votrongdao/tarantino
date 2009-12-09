using System.Collections.Generic;

namespace BatchJobs.Core
{
    public abstract class JobAgentBase<T> : IJobAgent where T : class
    {
        private readonly IStateTransitionFactory _factory;

        public JobAgentBase(IStateTransitionFactory factory)
        {
            _factory = factory;
        }

        public void Execute()
        {
            IEnumerable<IStateTransition<T>> transitions = _factory.GetAll<T>();
            T[] batches = GetNextEntites();

            foreach (T batch in batches)
            {
                foreach (var transition in transitions)
                {
                    if (transition.IsValid(batch))
                        transition.Execute(batch);
                }
            }
        }

        protected abstract T[] GetNextEntites();
    }
}