using System.Collections.Generic;
using System.IO;
using System.Linq;
using GF.DillyDally.Shared.Extensions;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.Core
{
    internal class TestData
    {
        private readonly Queue<string> _activityNames = ShuffleAndCreateQueueFor(new[]
            {"Rudern", "Malen", "PS4 Spielen", "Programmieren", "Kochen", "Aufräumen", "Staubsaugen", "Putzen"});

        private static Queue<string> ShuffleAndCreateQueueFor(IEnumerable<string> values)
        {
            var activityQueue = new Queue<string>();
            foreach (var val in values.Shuffle())
            {
                activityQueue.Enqueue(val);
            }

            return activityQueue;
        }

        internal string GetRandomActivityName()
        {
            return this._activityNames.Dequeue();
        }

        public string GetRandomImageFilePath()
        {
            var fileName = new List<string> {"TestImage.jpg"}.Shuffle().First();
            return Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);
        }

        public byte[] GetRandomImageBytes()
        {
            var randomFilePath = this.GetRandomImageFilePath();
            return File.ReadAllBytes(randomFilePath);
        }
    }
}