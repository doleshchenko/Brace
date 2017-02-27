namespace Brace.DomainModel
{
    public interface ITypeLink<TKey>
    {
        TKey LinkKey { get; set; }
    }
}