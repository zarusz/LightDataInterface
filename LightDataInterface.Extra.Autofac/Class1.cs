using Autofac;
using LightDataInterface.Core;

namespace LightDataInterface.Extra.Autofac
{
    public class LightDataInterfaceModule : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DataSessionHolder>()
                .InstancePerLifetimeScope();
        }

        #endregion
    }
}
