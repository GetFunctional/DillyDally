using System;
using System.ComponentModel;
using System.IO.Packaging;
using System.Windows;

namespace GF.DillyDally.Wpf.Client.Wpf
{
    public static class DesignTimeInvestigator
    {
        #region Constructors

        static DesignTimeInvestigator()
        {
            RegisterPackScheme();
        }

        #endregion

        #region Properties, Indexers

        public static bool IsInDesignMode
        {
            get
            {
                return (bool) DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject))
                    .DefaultValue;
            }
        }

        #endregion

        public static void RegisterPackScheme()
        {
            if (!UriParser.IsKnownScheme(PackUriHelper.UriSchemePack))
            {
                UriParser.Register(new GenericUriParser
                    (GenericUriParserOptions.GenericAuthority), PackUriHelper.UriSchemePack, -1);
            }
        }
    }
}