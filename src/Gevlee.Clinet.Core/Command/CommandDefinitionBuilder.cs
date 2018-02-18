﻿using System;
using Gevlee.Clinet.Core.Flag;

namespace Gevlee.Clinet.Core.Command
{
    internal class CommandDefinitionBuilder : ICommandDefinitionBuilder
    {
        private readonly CommandDefinition definition;
        public CommandDefinitionBuilder()
        {
            definition = new CommandDefinition();
        }
        
        public ICommandDefinitionBuilder WithShortName(string name)
        {
            definition.Short = name;
            return this;
        }

        public ICommandDefinitionBuilder WithLongName(string name)
        {
            definition.Long = name;
            return this;
        }

        public ICommandDefinitionBuilder WithDescription(string description)
        {
            definition.Description = description;
            return this;
        }

        public ICommandDefinitionBuilder WithFlag<TFlag>(Action<IFlagDefinitionBuilder> flagDefinitionBuildFunction)
            where TFlag : IFlag
        {
            var builder = new FlagDefinitionBuilder();
            flagDefinitionBuildFunction(builder);
            return WithFlag<TFlag>(builder.Build());
        }

        public ICommandDefinitionBuilder WithFlag<TFlag>(FlagDefinition definition) where TFlag : IFlag
        {
            this.definition.Flags.Add(definition, Activator.CreateInstance<TFlag>());
            return this;
        }

        public CommandDefinition Build()
        {
            return definition;
        }
    }
}