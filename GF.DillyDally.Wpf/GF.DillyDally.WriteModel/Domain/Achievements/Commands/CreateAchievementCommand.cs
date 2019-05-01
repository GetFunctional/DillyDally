using System;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class CreateAchievementCommand : IRequest<CreateAchievementResponse>
    {
        public CreateAchievementCommand(string name, int counterIncrease, int storypoints)
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