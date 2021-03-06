﻿using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers
{
    internal sealed class RunningNumberCounterAggregateRoot : AggregateRootBase
    {
        public RunningNumberCounterAggregateRoot()
        {
            this.RegisterTransition<RunningNumberCounterCreatedEvent>(this.Apply);
            this.RegisterTransition<AddNextNumberEvent>(this.Apply);
        }

        private RunningNumberCounterAggregateRoot(Guid runningNumberId, RunningNumberCounterArea counterArea,
            string prefix,
            int initialNumber) : this()
        {
            var creationEvent =
                new RunningNumberCounterCreatedEvent(runningNumberId, counterArea, prefix, initialNumber);
            this.RaiseEvent(creationEvent);
        }

        internal RunningNumberCounterArea CounterArea { get; private set; }
        internal string Prefix { get; private set; }
        internal int InitialNumber { get; private set; }
        internal List<RunningNumber> Numbers { get; } = new List<RunningNumber>();

        private void Apply(RunningNumberCounterCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Prefix = obj.Prefix;
            this.InitialNumber = obj.InitialNumber;
            this.CounterArea = obj.CounterArea;
        }

        private void Apply(AddNextNumberEvent obj)
        {
            var nextNumber = new RunningNumber(obj.NextNumberId, obj.Prefix, obj.NextNumberInRow);
            this.Numbers.Add(nextNumber);
        }

        internal static RunningNumberCounterAggregateRoot Create(Guid runningNumberId,
            RunningNumberCounterArea counterArea, string prefix,
            int initialNumber)
        {
            return new RunningNumberCounterAggregateRoot(runningNumberId, counterArea, prefix, initialNumber);
        }

        internal void AddNextNumber(Guid nextNumberId)
        {
            var nextNumberInRow = (this.Numbers.Any() ? this.Numbers.Max(x => x.Number) : this.InitialNumber) + 1;
            var nextNumberEvent = new AddNextNumberEvent(this.AggregateId, nextNumberId, this.CounterArea, this.Prefix,
                nextNumberInRow);
            this.RaiseEvent(nextNumberEvent);
        }
    }
}