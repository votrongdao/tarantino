using System;
using System.Collections.Generic;
using Tarantino.Core.Commons.Services.Logging;

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
        	Logger.Debug(this, "Getting transitions");
            IEnumerable<IStateTransition<T>> transitions = _factory.GetAll<T>();
			Logger.Debug(this, "Retrieved transitions");
            T[] batches = GetNextEntites();
			Logger.Debug(this, string.Format("Found {0} batches", batches.Length));

            foreach (T batch in batches)
            {
                foreach (var transition in transitions)
                {
					Logger.Debug(this, string.Format("Examining transition {0} in batch {1}", transition, batch));
					if (transition.IsValid(batch))
					{
						Logger.Debug(this, string.Format("Transition {0} is valid for batch {1}, executing", transition, batch));
						transition.Execute(batch);
					}
                }
            }
        }

        protected abstract T[] GetNextEntites();
    }
}