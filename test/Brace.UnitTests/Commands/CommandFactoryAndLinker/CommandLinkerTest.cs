﻿using System.Reflection;
using Brace.Commands;
using Brace.Commands.Factory;
using Brace.DomainService.TypeLinker;
using Brace.Stub.CommandLinker;
using Xunit;

namespace Brace.UnitTests.Commands.CommandFactoryAndLinker
{
    public class CommandLinkerTest
    {
        private readonly CommandLinker _commandLinker;

        public CommandLinkerTest()
        {
            _commandLinker = new CommandLinker(typeof(CommandStubMain).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetCommandType_ValidCommand_ReturnsCommand()
        {
            var command = CommandType.GetContent.ToString();
            var commandType = _commandLinker.GetCommandType(command);
            Assert.Equal(typeof(GetContentCommand), commandType);
        }

        [Fact]
        public void GetCommandType_InvalidCommand_ReturnsCommand()
        {
            var command = "invalidcommand";
            var result = Assert.Throws<LinkerException>(() => _commandLinker.GetCommandType(command));
            Assert.Equal($"Invalid command identifier - {command}. Cannot translate into CommandType.", result.Message);
        }
    }
}