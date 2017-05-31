using System.Collections;
using System.Linq;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Moq;
using Xunit;

namespace Brace.UnitTests.DomainModel.DocumentProcessing.Decorator.Content
{
    public class DocumentListContentTest
    {
        [Fact]
        public void Add_ContentItem_AddsItemToInternalCollection()
        {
            var contentList = new DocumentListContent<DocumentContent>();
            var itemToAdd = Mock.Of<DocumentContent>();
            contentList.Add(itemToAdd);
            var firstItemFromCollection = contentList.FirstOrDefault();
            Assert.Equal(itemToAdd, firstItemFromCollection);
            Assert.Equal(1, contentList.Count());
        }

        [Fact]
        public void AddRange_RangeOfContentItems_AddsItemRangeToInternalCollection()
        {
            var contentList = new DocumentListContent<DocumentContent>();
            var itemToAdd1 = Mock.Of<DocumentContent>();
            var itemToAdd2 = Mock.Of<DocumentContent>();
            var itemToAdd3 = Mock.Of<DocumentContent>();
            var range = new[] {itemToAdd1, itemToAdd2, itemToAdd3};
            contentList.AddRange(range);
            Assert.Equal(3, contentList.Count());
            Assert.All(range, content =>
            {
                Assert.True(contentList.Contains(content));
            });
        }

        [Fact]
        public void Remove_ItemFromTheCollection_RemovesItem()
        {
            var contentList = new DocumentListContent<DocumentContent>();
            var itemToAddAndRemove = Mock.Of<DocumentContent>();
            contentList.Add(itemToAddAndRemove);
            contentList.Remove(itemToAddAndRemove);
            Assert.Equal(0, contentList.Count());
        }

        [Fact]
        public void ContentAsString_CollectionOfItems_ReturnsFormatedString()
        {
            var contentList = new DocumentListContent<DocumentContent>();
            var itemToAdd1 = new Mock<DocumentContent>();
            var itemToAdd2 = new Mock<DocumentContent>();
            var itemToAdd3 = new Mock<DocumentContent>();
            itemToAdd1.Setup(it => it.ContentAsString()).Returns(nameof(itemToAdd1));
            itemToAdd2.Setup(it => it.ContentAsString()).Returns(nameof(itemToAdd2));
            itemToAdd3.Setup(it => it.ContentAsString()).Returns(nameof(itemToAdd3));
            var range = new[] { itemToAdd1.Object, itemToAdd2.Object, itemToAdd3.Object };
            contentList.AddRange(range);
            var result = contentList.ContentAsString();
            var expected = string.Join("\n", range.Select(it => it.ContentAsString()));
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetEnumerator_ReturnsEnumerator()
        {
            var contentList = new DocumentListContent<DocumentContent>();
            var itemToAdd = Mock.Of<DocumentContent>();
            contentList.Add(itemToAdd);
            var enumerator = ((IEnumerable) contentList).GetEnumerator();
            Assert.NotNull(enumerator);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                Assert.Equal(itemToAdd, current);
            }
        }
    }
}