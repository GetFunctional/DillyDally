using System;
using System.Linq;
using GF.DillyDally.Update;
using GF.DillyDally.Update.UpdateSteps;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.SqlDbAccess
{
    [TestFixture]
    internal class SqlScriptSelectorTests
    {
        [Test]
        public void ScriptSelector_GetUpdateStepsBeginningFromVersion_ShouldFindAtleastOneScript()
        {
            // Arrange
            var scriptSelector = new SqlScriptSelector();
            var versionBegin = new Version(1, 0, 0, 0);

            // Act
            var steps = scriptSelector.GetUpdateStepsBeginningFromVersion(versionBegin);

            // Assert
            Assert.That(steps.Count, Is.GreaterThan(0));
            Assert.That(steps.All(x => x.SqlCommands.Count > 0), Is.EqualTo(true));
        }

        [Test]
        public void ScriptSelector_GetUpdateStepsBeginningFromVersion_ShouldFindAtleastOneStep()
        {
            // Arrange
            var scriptSelector = new SqlScriptSelector();
            var versionBegin = new Version(1, 0, 0, 0);

            // Act
            var updateSteps = scriptSelector.GetUpdateStepsBeginningFromVersion(versionBegin);

            // Assert
            Assert.That(updateSteps.Count, Is.GreaterThan(0));
        }
    }
}