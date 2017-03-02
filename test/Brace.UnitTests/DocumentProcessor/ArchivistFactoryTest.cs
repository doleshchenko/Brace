using System.Collections.Generic;
using Brace.DocumentProcessor.Exceptions;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService;
using Brace.DomainService.DocumentProcessor;
using Brace.UnitTests.DocumentProcessor.Stub;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor
{
    public class ArchivistFactoryTest
    {
        [Theory]
        [MemberData(nameof(NullOrEmptyArrayOfActions))]
        public void CreateArchivistChain_NullOrEmptyArrayOfActions_ReturnsDoNothingArchivist(string[] actions)
        {
            var expectedArchivist = new DoNothingArchivist();
            var archivistProviderStub = new Mock<ISingleInterfaceServiceProvider<IArchivist>>();
            var archivistLinkerStub = new Mock<IArchivistLinker>();
            archivistLinkerStub.Setup(linker => linker.GetArchivistType(ArchivistType.DoNothing)).Returns(expectedArchivist.GetType);
            archivistProviderStub.Setup(provider => provider.Resolve(expectedArchivist.GetType())).Returns(expectedArchivist);
            var archivistFactory = new ArchivistFactory(archivistProviderStub.Object, archivistLinkerStub.Object);
            var result = archivistFactory.CreateArchivistChain(actions);
            Assert.Equal(expectedArchivist, result);
        }

        [Fact]
        public void CreateArchivistChain_IncorrectAction_ThrowsDocumentProcessorException()
        {
            var archivistProviderStub = new Mock<ISingleInterfaceServiceProvider<IArchivist>>();
            var archivistLinkerStub = new Mock<IArchivistLinker>();
            var archivistFactory = new ArchivistFactory(archivistProviderStub.Object, archivistLinkerStub.Object);
            var result = Assert.Throws<DocumentProcessorException>(() => archivistFactory.CreateArchivistChain(new [] { "fake", "action" }));
            Assert.Equal("Invalid action (command parameter) \"fake,action\". Cannot find corresponding Archivist type.", result.Message);
        }

        [Fact]
        public void CreateArchivistChain_CorrectOneAction_ReturnsArchivist()
        {
            var parameters = new[] { "encrypt" };
            var expectedArchivist = new EncryptArchivist();
            var archivistProviderStub = new Mock<ISingleInterfaceServiceProvider<IArchivist>>();
            var archivistLinkerStub = new Mock<IArchivistLinker>();
            archivistLinkerStub.Setup(linker => linker.GetArchivistType(ArchivistType.Encrypt)).Returns(expectedArchivist.GetType);
            archivistProviderStub.Setup(provider => provider.Resolve(expectedArchivist.GetType())).Returns(expectedArchivist);
            var archivistFactory = new ArchivistFactory(archivistProviderStub.Object, archivistLinkerStub.Object);
            var result = archivistFactory.CreateArchivistChain(parameters);
            Assert.Equal(expectedArchivist, result);
        }

        public static IEnumerable<object[]> NullOrEmptyArrayOfActions
        {
            get
            {
                yield return new object[] { null };
                yield return new object[] { new string[0] };
            }
        }
    }
}