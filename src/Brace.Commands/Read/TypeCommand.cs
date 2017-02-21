using System.Threading.Tasks;
using Brace.TypeExtension;

namespace Brace.Commands.Read
{
    public class TypeCommand : IBraceCommand
    {
        public async Task<string> ExecuteAsync(string argument, string[] parameters)
        {
            return "Type command executed";
        }

        public string Name => ReadCommands.Type.ToStringUpper();
    }
}
