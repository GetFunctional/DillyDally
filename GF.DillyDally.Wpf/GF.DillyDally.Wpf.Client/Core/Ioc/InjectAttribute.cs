using System;

namespace GF.DillyDally.Wpf.Client.Core.Ioc
{
    /// <summary>
    ///     Indicates a required property dependency or a named constructor dependency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    internal sealed class InjectAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InjectAttribute" /> class.
        /// </summary>
        internal InjectAttribute()
            : this(string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InjectAttribute" /> class.
        /// </summary>
        /// <param name="serviceName">The name of the service to be injected.</param>
        internal InjectAttribute(string serviceName)
        {
            this.ServiceName = serviceName;
        }

        /// <summary>
        ///     Gets the name of the service to be injected.
        /// </summary>
        internal string ServiceName { get; private set; }
    }
}