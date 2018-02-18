﻿using Gevlee.Clinet.Core.Command;
using Gevlee.Clinet.Core.Parsing;
using Moq;
using Xunit;

namespace Gevlee.Clinet.Core.Tests
{
    public class CommandRunnerTest
    {
        private readonly Mock<IArgsDescriber> argsDescriberMock;
        private readonly Mock<ICommandRegistry> commandRegistryMock;

        public CommandRunnerTest()
        {
            argsDescriberMock = new Mock<IArgsDescriber>();
            commandRegistryMock = new Mock<ICommandRegistry>();

            ObjectFactory.ArgDescriberFactory = enumerable => argsDescriberMock.Object;
        }
        [Fact]
        public void Run_ShouldInvoke_Command()
        {
            CommandDefinition commandDefinition = new CommandDefinition("t", "test") { };
            var commandMock = new Mock<ICommand>();
            argsDescriberMock.Setup(x => x.Describe(It.IsAny<string[]>())).Returns(() => new ArgsDescriptionResult
            {
                CommandDefinition = commandDefinition
            });
            commandRegistryMock.Setup(x => x.GetCommand(commandDefinition)).Returns(() => commandMock.Object);

            CreateObject().Run(new string[0]);
            
            commandMock.Verify(x => x.Run(It.IsAny<CommandContext>()), Times.Once);
        }

        private CommandRunner CreateObject()
        {
            return new CommandRunner(commandRegistryMock.Object);
        }
    }
}