using System;
using log4net;

namespace GF.DillyDally.Wpf.Client.Core.ApplicationState
{
    public class ExceptionSolver
    {
        private readonly ILog _exceptionLogger = LogManager.GetLogger(typeof(ExceptionSolver));

        internal void Solve(Exception ex)
        {
            var exceptionTraced = this.Traced(ex);
            if (!exceptionTraced)
            {
                this.TraceException(ex);
#if DEBUG
                throw ex;
#endif
            }
        }

        private void AddDebugInformation(Exception exception, string key, object value)
        {
            if (!exception.Data.Contains(key))
            {
                exception.Data.Add(key, value);
            }
        }

        private const string TracedSymbol = "Traced";
        private void SetTraced(Exception exception)
        {
            this.AddDebugInformation(exception, TracedSymbol, true);
        }

        private bool Traced(Exception exception)
        {
            return exception.Data.Contains(TracedSymbol);
        }

        private void TraceException(Exception exception)
        {
            if (!this.Traced(exception))
            {
                this._exceptionLogger.Debug(string.Empty, exception);
                this.SetTraced(exception);
            }
        }
    }
}