using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class SqlScriptSelector
    {
        private const string AssemblyName = @"GF.DillyDally.Data.Sqlite";
        private const string ScriptsNamespace = AssemblyName + ".Scripts";
        private readonly Assembly _assembly = Assembly.GetAssembly(typeof(SqlScriptSelector));

        public IList<UpdateStep> GetUpdateStepsBeginningFromVersion(Version versionBegin)
        {
            var updateSteps = new List<UpdateStep>();

            foreach (var embeddedScript in this.FindEmbeddedScripts())
            {
                var fileNameWithoutExtension = this.GetVersionFromScript(embeddedScript);
                var updateStepVersion =
                    Version.Parse(fileNameWithoutExtension ?? throw new InvalidOperationException());
                var fullResourceName = $"{ScriptsNamespace}.{embeddedScript}";

                using (var sqlScriptStream = this._assembly.GetManifestResourceStream(fullResourceName))
                {
                    var scriptStreamReader = new SqlScriptReader(sqlScriptStream);
                    var updateStep = new UpdateStep(updateStepVersion, new List<string>(scriptStreamReader.ToList()));
                    updateSteps.Add(updateStep);
                }
            }

            return updateSteps;
        }

        private string GetVersionFromScript(string embeddedScript)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(embeddedScript);
            if (!Regex.IsMatch(fileNameWithoutExtension ?? throw new InvalidOperationException(), @"^[0-9.]+$"))
            {
                throw new ArgumentException("embeddedScript has wrong format. Should be like '1.1.1.1.sql'");
            }

            return fileNameWithoutExtension;
        }

        private IEnumerable<string> GetManifestResourceNames(string nameSpace)
        {
            return this._assembly.GetManifestResourceNames().Select(item => item.Substring(nameSpace.Length + 1));
        }

        private IList<string> FindEmbeddedScripts()
        {
            return
                this.GetManifestResourceNames(ScriptsNamespace)
                    .Where(res => res.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                    .Distinct()
                    .OrderBy(name => new Version(this.GetVersionFromScript(name))).ToList();
        }
    }
}