using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Shared.Extensions;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.Core
{
    internal class TestData
    {
        //private readonly ConcurrentQueue<string> _activityNames = ShuffleAndCreateQueueFor(new[]
        //    {"Rudern", "Malen", "PS4 Spielen", "Programmieren", "Kochen", "Aufräumen", "Staubsaugen", "Putzen"});

        //private static ConcurrentQueue<string> ShuffleAndCreateQueueFor(IEnumerable<string> values)
        //{
        //    var activityQueue = new ConcurrentQueue<string>();
        //    foreach (var val in values.Shuffle())
        //    {
        //        activityQueue.Enqueue(val);
        //    }

        //    return activityQueue;
        //}

        //internal async Task<string> GetRandomActivityNameAsync(ActivityService activityService)
        //{
        //    string name;
        //    if (this._activityNames.TryDequeue(out name))
        //    {
        //        var response = await activityService.CanCreateActivityAsync(name);
        //        if (response.CanCreate)
        //        {
        //            return name;
        //        }

        //        return await this.GetRandomActivityNameAsync(activityService);
        //    }

        //    throw new NotSupportedException();
        //}

        //public string GetRandomImageFilePath()
        //{
        //    var fileName = new List<string> {"TestImage.jpg"}.Shuffle().First();
        //    return Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);
        //}

        //public byte[] GetRandomImageBytes()
        //{
        //    var randomFilePath = this.GetRandomImageFilePath();
        //    return File.ReadAllBytes(randomFilePath);
        //}
    }
}