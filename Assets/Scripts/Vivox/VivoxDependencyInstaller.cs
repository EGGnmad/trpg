using VContainer;
using VContainer.Unity;

namespace TRPG.Vivox
{
    public class VivoxDependencyInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<VivoxClient>();
        }
    }
}