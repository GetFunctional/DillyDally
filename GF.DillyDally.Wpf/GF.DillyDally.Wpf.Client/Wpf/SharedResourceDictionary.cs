using System;
using System.Collections.Generic;
using System.Windows;

namespace GF.DillyDally.Wpf.Client.Wpf
{
    /// <summary>
    ///     The shared resource dictionary is a specialized resource dictionary
    ///     that loads it content only once. If a second instance with the same source
    ///     is created, it only merges the resources from the cache.
    /// </summary>
    public sealed class SharedResourceDictionary : ResourceDictionary
    {
        #region - Methoden oeffentlich -

        public static void ClearCache()
        {
            InternalSharedCache.Clear();
        }

        #endregion

        #region - Methoden privat -

        private static void CacheSource(Uri source, ResourceDictionary dictionary)
        {
            if (SharedCache.ContainsKey(source))
            {
                InternalSharedCache.Remove(source);
            }

            InternalSharedCache.Add(source, new WeakReference(dictionary, false));
        }

        #endregion

        #region - Felder privat -

        private static readonly Dictionary<Uri, WeakReference> InternalSharedCache =
            new Dictionary<Uri, WeakReference>();

        private Uri _sourceCore;

        #endregion

        #region - Konstruktoren -

        public SharedResourceDictionary()
        {
        }

        public SharedResourceDictionary(Uri source)
        {
            this.Source = source;
        }

        #endregion

        #region - Properties oeffentlich -

        /// <summary>
        ///     Internal cache of loaded dictionaries
        /// </summary>
        public static IReadOnlyDictionary<Uri, WeakReference> SharedCache
        {
            get { return InternalSharedCache; }
        }

        /// <summary>
        ///     Gets or sets the uniform resource identifier (URI) to load resources from.
        /// </summary>
        public new Uri Source
        {
            get { return this._sourceCore; }
            set
            {
                this._sourceCore = value;
                if (value == null)
                {
                    return;
                }

                if (!SharedCache.ContainsKey(this._sourceCore) || DesignTimeInvestigator.IsInDesignMode)
                {
                    base.Source = this._sourceCore;

                    if (!DesignTimeInvestigator.IsInDesignMode)
                    {
                        CacheSource(base.Source, this);
                    }
                }
                else
                {
                    var resourceDictionary = (ResourceDictionary) SharedCache[this._sourceCore].Target;
                    if (resourceDictionary != null)
                    {
                        this.MergedDictionaries.Add(resourceDictionary);
                        this._sourceCore = resourceDictionary.Source;
                    }
                    else
                    {
                        base.Source = this._sourceCore;
                        if (!DesignTimeInvestigator.IsInDesignMode)
                        {
                            CacheSource(base.Source, this);
                        }
                    }
                }
            }
        }

        #endregion
    }
}