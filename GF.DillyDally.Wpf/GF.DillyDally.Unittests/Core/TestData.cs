using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GF.DillyDally.Shared.Extensions;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.Core
{
    internal class TestData
    {
        private readonly ConcurrentQueue<string> _activityNames = ShuffleAndCreateQueueFor(new[]
            {"Rudern", "Malen", "PS4 Spielen", "Programmieren", "Kochen", "Aufräumen", "Staubsaugen", "Putzen"});

        private static ConcurrentQueue<string> ShuffleAndCreateQueueFor(IEnumerable<string> values)
        {
            var activityQueue = new ConcurrentQueue<string>();
            foreach (var val in values.Shuffle())
            {
                activityQueue.Enqueue(val);
            }

            return activityQueue;
        }

        internal string GetRandomActivityName()
        {
            string name;
            if (this._activityNames.TryDequeue(out name))
            {
                return name;
            }

            throw new NotSupportedException();
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