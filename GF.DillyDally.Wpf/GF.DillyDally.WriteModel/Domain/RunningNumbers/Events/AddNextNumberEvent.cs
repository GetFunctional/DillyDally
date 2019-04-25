﻿using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Events
{
    public sealed class AddNextNumberEvent : AggregateEventBase
    {
        public AddNextNumberEvent(Guid aggregateId, Guid nextNumberId, string prefix, int nextNumberInRow) : base(
            aggregateId)
        {
            this.NextNumberId = nextNumberId;
            this.Prefix = prefix;
            this.NextNumberInRow = nextNumberInRow;
        }

        public Guid NextNumberId { get; }
        public string Prefix { get; }
        public int NextNumberInRow { get; }
    }
}