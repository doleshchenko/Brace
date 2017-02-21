using System.Threading.Tasks;

namespace Brace.Commands
{
    public interface IBraceCommand
    {
        Task<string> ExecuteAsync(string argument, string[] parameters);
        string Name { get; }
    }
}
