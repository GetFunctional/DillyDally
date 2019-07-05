using System;
using log4net;

namespace GF.DillyDally.Wpf.Client.Core.Exceptions
{
    internal static class ExceptionEvaluator
    {
        private static readonly ILog ExceptionLogger = LogManager.GetLogger(typeof(ExceptionEvaluator));

        internal static void Evaluate(Exception ex)
        {
            ex.TraceException();
        }

        private static void AddDebugInformation(this Exception exception, string key, object value)
        {
            if (!exception.Data.Contains(key))
            {
                exception.Data.Add(key, value);
            }
        }

        private static void SetTraced(this Exception exception)
        {
            exception.AddDebugInformation("Traced", true);
        }

        private static bool Traced(this Exception exception)
        {
            return exception.Data.Contains("Traced");
        }

        public static void TraceException(this Exception exception)
        {
            if (!exception.Traced())
            {
                ExceptionLogger.Debug(string.Empty, exception);
                exception.SetTraced();
            }
        }
    }
}