using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.Guideline
{
    [TestFixture]
    public class EventHandlerGuideline
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._infrastructureSetup.Setup(UnittestsSetup.ExampleDatabase);
        }

        #endregion

        private readonly InfrastructureTestSetup _infrastructureSetup = new InfrastructureTestSetup();

        [Test]
        public void AllEventHandler_AreRegistered()
        {
            // Arrange
            var baseTypeForEvents = typeof(IAggregateEvent);
            var eventImplementations = Assembly.GetAssembly(typeof(AggregateEventBase)).GetTypes()
                .Where(type => baseTypeForEvents.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                .ToList();

            var eventsWithoutRegistration = new List<Type>();
            var eventDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IEventDispatcher>();

            // Act
            foreach (var eventImplementation in eventImplementations)
            {
                if (!eventDispatcher.HasHandlerForEvent(eventImplementation))
                {
                    eventsWithoutRegistration.Add(eventImplementation);
                }
            }

            // Assert
            Assert.That(eventsWithoutRegistration.Count, Is.EqualTo(4));
        }
    }
}