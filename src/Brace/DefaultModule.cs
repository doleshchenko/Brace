using System.Reflection;
using Autofac;
using Brace.Commands;
using Brace.Commands.Factory;
using Brace.DocumentProcessor;
using Brace.DocumentProcessor.Strategies;
using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainService;
using Brace.DomainService.DocumentProcessor;
using Brace.Interpretation;
using Brace.Repository;
using Brace.Repository.Interface;
using Module = Autofac.Module;

namespace Brace
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandInterpreter>().As<ICommandInterpreter>().SingleInstance();
            builder.RegisterType<DocumentProcessor.DocumentProcessor>().As<IDocumentProcessor>().SingleInstance();

            builder.RegisterType<SingleInterfaceServiceProvider<IDocumentProcessingStrategy>>().As<ISingleInterfaceServiceProvider<IDocumentProcessingStrategy>>().SingleInstance();
            builder.RegisterType<SingleInterfaceServiceProvider<IArchivist>>().As<ISingleInterfaceServiceProvider<IArchivist>>().SingleInstance();
            builder.RegisterType<SingleInterfaceServiceProvider<ICommand>>().As<ISingleInterfaceServiceProvider<ICommand>>().SingleInstance();

            builder.RegisterType<CommandLinker>().As<ICommandLinker>().WithParameter("assembly", typeof(CommandBase).GetTypeInfo().Assembly).SingleInstance();
            builder.RegisterType<ArchivistLinker>().As<IArchivistLinker>().WithParameter("assembly", typeof(Archivist).GetTypeInfo().Assembly).SingleInstance();
            builder.RegisterType<DocumentProcessingStrategyTypeLinker>().As<IDocumentProcessingStrategyTypeLinker>().WithParameter("assembly", typeof(PrintDocumentStrategy).GetTypeInfo().Assembly).SingleInstance();

            builder.RegisterType<ArchivistFactory>().As<IArchivistFactory>().SingleInstance();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();

            builder.RegisterType<DocumentRepository>().As<IDocumentRepository>();

            builder.RegisterType<PrintDocumentStrategy>();
            builder.RegisterType<DoNothingArhivist>();

            var commandAssemblies = typeof(ICommand).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(commandAssemblies).Where(t => t.Namespace.Contains("Brace.Commands.CommandImplementation"));
        }
    }
}