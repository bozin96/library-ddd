using Autofac;

namespace Library.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ToDoItemSearchService>()
            //    .As<IToDoItemSearchService>().InstancePerLifetimeScope();
        }
    }
}
