using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brace.DocumentProcessor.Exceptions;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainService;
using Brace.DomainService.DocumentProcessor;
using Brace.UnitTests.DocumentProcessor.Mock;
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
            var expectedArchivist = new DoNothingArchivistStub(null);
            var archivistProviderStub = new Mock<ISingleInterfaceServiceProvider<IArchivist>>();
            var archivistLinkerStub = new Mock<IArchivistLinker>();
            archivistLinkerStub.Setup(linker => linker.GetArchivistType(ArchivistType.DoNothing)).Returns(expectedArchivist.GetType);
            archivistProviderStub.Setup(provider => provider.Resolve(expectedArchivist.GetType())).Returns(expectedArchivist);
            var archivistFactory = new ArchivistFactory(archivistProviderStub.Object, archivistLinkerStub.Object);
            var result = archivistFactory.CreateArchivistChain(actions);
            Assert.Equal(expectedArchivist, result);
        }

        [Fact]
        public void CreateArchivistChain_CorrectOneAction_ReturnsArchivist()
        {
            var parameters = new[] { "encrypt" };
            var expectedArchivist = new EncryptArchivistStub(null);
            var archivistProviderStub = new Mock<ISingleInterfaceServiceProvider<IArchivist>>();
            var archivistLinkerStub = new Mock<IArchivistLinker>();
            archivistLinkerStub.Setup(linker => linker.GetArchivistType(ArchivistType.Encrypt)).Returns(expectedArchivist.GetType);
            archivistProviderStub.Setup(provider => provider.Resolve(expectedArchivist.GetType())).Returns(expectedArchivist);
            var archivistFactory = new ArchivistFactory(archivistProviderStub.Object, archivistLinkerStub.Object);
            var result = archivistFactory.CreateArchivistChain(parameters);
            Assert.Equal(expectedArchivist, result);
        }

        [Fact]
        public void CreateArchivistChain_CorrectSeveralActions_ReturnsArchivistChain()
        {
            var parameters = new[] {"decrypt", "getstatus" };
            var decryptArchivistMock = new DecryptArchivistMock(null);
            var getStatusArchivsitMock = new GetStatusArchivistMock(null);
            var archivistProviderStub = new Mock<ISingleInterfaceServiceProvider<IArchivist>>();
            var archivistLinkerStub = new Mock<IArchivistLinker>();
            archivistProviderStub.Setup(provider => provider.Resolve(decryptArchivistMock.GetType())).Returns(decryptArchivistMock);
            archivistProviderStub.Setup(provider => provider.Resolve(getStatusArchivsitMock.GetType())).Returns(getStatusArchivsitMock);
            archivistLinkerStub.Setup(linker => linker.GetArchivistType(ArchivistType.Decrypt)).Returns(decryptArchivistMock.GetType);
            archivistLinkerStub.Setup(linker => linker.GetArchivistType(ArchivistType.GetStatus)).Returns(getStatusArchivsitMock.GetType);
            var archivistFactory = new ArchivistFactory(archivistProviderStub.Object, archivistLinkerStub.Object);
            var root = archivistFactory.CreateArchivistChain(parameters);
            var successor1 = ((DecryptArchivistMock) root).GetSuccessor();
            var successor2 = ((GetStatusArchivistMock) successor1)?.GetSuccessor();
            Assert.Equal(root, decryptArchivistMock);
            Assert.NotNull(successor1);
            Assert.Null(successor2);
            Assert.Equal(successor1, getStatusArchivsitMock);
        }

        [Theory]
        [MemberData(nameof(InvalidActionsArray))]
        public void CreateArchivistChain_IncorrectAction_ThrowsDocumentProcessorException(string[] actions)
        {
            var archivistProviderStub = new Mock<ISingleInterfaceServiceProvider<IArchivist>>();
            var archivistLinkerStub = new Mock<IArchivistLinker>();
            var archivistFactory = new ArchivistFactory(archivistProviderStub.Object, archivistLinkerStub.Object);
            var result = Assert.Throws<DocumentProcessorException>(() => archivistFactory.CreateArchivistChain(actions));
            var fields = typeof(ArchivistType).GetTypeInfo().GetFields().ToArray();
            var fieldsWithAttributes = fields.Select(it => new { Attribure = it.GetCustomAttribute<ArchivistTypeDescriptionAttribute>() })
                    .Where(it => it.Attribure != null)
                    .ToArray();

            var wrongItems = actions.Except(fieldsWithAttributes.Select(it => it.Attribure.ArchivistActionName)).ToArray();
            Assert.Equal($"Invalid action (command parameter) \"{string.Join(",", wrongItems)}\". Cannot find corresponding Archivist type.", result.Message);
        }

        public static IEnumerable<object[]> NullOrEmptyArrayOfActions
        {
            get
            {
                yield return new object[] { null };
                yield return new object[] { new string[0] };
            }
        }

        public static IEnumerable<object[]> InvalidActionsArray
        {
            get
            {
                yield return new object[] { new[] {"fake", "action"} }; //both invalid
                yield return new object[] { new[] { "fake", "encrypt"} }; //first valid, second invalid
                yield return new object[] { new[] { "decrypt", "fake"} }; //second valid, first invalid
            }
        }
    }
}