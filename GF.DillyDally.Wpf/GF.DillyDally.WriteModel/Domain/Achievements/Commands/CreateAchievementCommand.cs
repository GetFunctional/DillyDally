using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class CreateAchievementCommand : AggregateCommandBase
    {
        public CreateAchievementCommand(string name, int counterIncrease, int storypoints) : base(Guid.Empty)
        {
            this.Name = name;
            this.CounterIncrease = counterIncrease;
            this.Storypoints = storypoints;
        }

        public string Name { get; }
        public int CounterIncrease { get; }
        public int Storypoints { get; }
    }
}