using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GF.DillyDally.WriteModel.Infrastructure;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.Guideline
{
    [TestFixture]
    public class EventClassGuideline
    {
        [Test]
        public void AllEventClasses_ShouldHaveAnAggregateIdParameterFirst()
        {
            // Arrange
            var baseTypeForEvents = typeof(IAggregateEvent);
            var eventImplementations = Assembly.GetAssembly(typeof(AggregateEventBase)).GetTypes()
                .Where(type => baseTypeForEvents.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                .ToList();

            var eventsDisregardingGuidelines = new List<Type>();

            // Act
            foreach (var eventImplementation in eventImplementations)
            {
                var constructorParameters = eventImplementation.GetConstructors().FirstOrDefault();
                var parameters = constructorParameters.GetParameters();

                if (parameters.First().Name != "aggregateId")
                {
                    eventsDisregardingGuidelines.Add(eventImplementation);
                    continue;
                }

                if (parameters.First().ParameterType != typeof(Guid))
                {
                    eventsDisregardingGuidelines.Add(eventImplementation);
                    continue;
                }
            }

            // Assert
            Assert.That(eventsDisregardingGuidelines.Count, Is.EqualTo(0));
        }
    }
}