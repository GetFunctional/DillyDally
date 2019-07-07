using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Update.UpdateSteps.SqlScripts;

namespace GF.DillyDally.Update.UpdateSteps
{
    public sealed class SqlScriptSelector
    {
        private readonly Assembly _assembly = Assembly.GetAssembly(typeof(SqlScriptSelector));

        public IList<SqlScript> GetUpdateStepsBeginningFromVersion(Version versionBegin, Version versionEnd = null)
        {
            var updateSteps = new List<SqlScript>();

            foreach (var embeddedScript in this.FindEmbeddedScripts(versionBegin, versionEnd).ToList())
            {
                var fileNameWithoutExtension = this.GetVersionFromScript(embeddedScript);
                var updateStepVersion =
                    Version.Parse(fileNameWithoutExtension ?? throw new InvalidOperationException());
                var fullResourceName = $"{GetScriptsNamespace(this._assembly)}.{embeddedScript}";

                using (var sqlScriptStream = this._assembly.GetManifestResourceStream(fullResourceName))
                {
                    var scriptStreamReader = new SqlScriptReader(sqlScriptStream);
                    var updateStep = new SqlScript(updateStepVersion, new List<string>(scriptStreamReader.ToList()));
                    updateSteps.Add(updateStep);
                }
            }

            return updateSteps;
        }

        private static string GetScriptsNamespace(Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            return assemblyName.Name + ".UpdateSteps.SqlScripts";
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

        private IEnumerable<string> FindEmbeddedScripts(Version fromVersion, Version toVersion)
        {
            var scriptsNameSpace = GetScriptsNamespace(this._assembly);
            return
                this.GetManifestResourceNames(scriptsNameSpace)
                    .Where(res => res.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                    .Distinct()
                    .Select(scriptName => new
                        {Version = new Version(this.GetVersionFromScript(scriptName)), ScriptName = scriptName})
                    .Where(script =>
                        script.Version >= fromVersion && (toVersion == null || script.Version <= toVersion))
                    .OrderBy(script => script.Version).Select(x => x.ScriptName);
        }

        public IList<string> GetScriptsForVersion(Version version)
        {
            return this.GetUpdateStepsBeginningFromVersion(version, version).SelectMany(x => x.SqlCommands).ToList();
        }
    }
}