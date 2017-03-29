using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Brace.DomainModel.DocumentProcessing.Decorator.Content
{
    public class DocumentListContent<TContent> : DocumentContent, IEnumerable<TContent> where TContent : DocumentContent
    {
        private readonly List<TContent> _contentItems;
        
        public DocumentListContent()
        {
            _contentItems = new List<TContent>();
        }

        public void Add(TContent item)
        {
            _contentItems.Add(item);
        }

        public void AddRange(IEnumerable<TContent> items)
        {
            _contentItems.AddRange(items);
        }

        public void Remove(TContent item)
        {
            _contentItems.Remove(item);
        }
            
        public override string ContentAsString()
        {
            return string.Join("\n", _contentItems.Select(it => it.ContentAsString()));
        }

        public IEnumerator<TContent> GetEnumerator()
        {
            return _contentItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _contentItems.GetEnumerator();
        }
    }
}