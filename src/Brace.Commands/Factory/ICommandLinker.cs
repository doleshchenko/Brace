using System;

namespace Brace.Commands.Factory
{
    public interface ICommandLinker
    {
        Type GetCommandType(string command);
    }
}