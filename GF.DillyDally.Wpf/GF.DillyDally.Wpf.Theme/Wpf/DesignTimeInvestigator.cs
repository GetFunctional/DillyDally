using System;
using System.ComponentModel;
using System.IO.Packaging;
using System.Windows;

namespace GF.DillyDally.Wpf.Theme.Wpf
{
    public static class DesignTimeInvestigator
    {
        static DesignTimeInvestigator()
        {
            RegisterPackScheme();
        }

        public static bool IsInDesignMode
        {
            get
            {
                return (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject))
                    .DefaultValue;
            }
        }

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