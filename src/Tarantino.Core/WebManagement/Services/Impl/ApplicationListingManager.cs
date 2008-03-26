using StructureMap;
using Tarantino.Core.WebManagement.Services.Views;

namespace Tarantino.Core.WebManagement.Services.Impl
{
    [Pluggable(ServiceKeys.Default)]
    public class ApplicationListingManager : IApplicationListingManager
    {
        private readonly IApplicationListingView view;

        public ApplicationListingManager(IApplicationListingView view)
        {
            this.view = view;
        }

        public void HandleRequest()
        {
            view.Render();
        }
    }
}