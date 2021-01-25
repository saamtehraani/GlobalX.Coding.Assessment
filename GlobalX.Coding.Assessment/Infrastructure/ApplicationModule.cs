using Autofac;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Services;

namespace GlobalX.Coding.Assessment.Infrastructure
{
    public class ApplicationModule : Module
    {
        private readonly string _filePath;
        public ApplicationModule(
                string filePath
            )
        {
            _filePath = filePath;
        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Services
            builder.Register(c => new ReadFileService(_filePath)).As<IReadFileService>().InstancePerLifetimeScope();
            builder.Register(c => new ValidateService(_filePath, c.Resolve<IReadFileService>())).As<IValidateService>().InstancePerLifetimeScope();
            builder.RegisterType<SortService>().As<ISortService>().InstancePerLifetimeScope();
            builder.RegisterType<WriteFileService>().As<IWriteFileService>().InstancePerLifetimeScope();
            #endregion
        }
    }
}
