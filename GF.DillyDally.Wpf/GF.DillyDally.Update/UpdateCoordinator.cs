using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Update.UpdateSteps;

namespace GF.DillyDally.Update
{
    public sealed class UpdateCoordinator
    {
        private readonly ISet<IUpdateStep> _versionSteps;

        public UpdateCoordinator()
        {
            this._versionSteps = this.ComposeVersionSteps();
        }

        private ISet<IUpdateStep> ComposeVersionSteps()
        {
            var versions = new HashSet<IUpdateStep>();
            versions.Add(new Version1000());
            return versions;
        }

        public ISet<IUpdateStep> GetUpdateStepsBeginningFromVersion(Version version)
        {
            return new HashSet<IUpdateStep>(this._versionSteps.Where(x => x.Version >= version));
        }
    }
}