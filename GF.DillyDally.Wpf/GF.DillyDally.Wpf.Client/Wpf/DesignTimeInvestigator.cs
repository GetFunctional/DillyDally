using System;
using System.ComponentModel;
using System.Windows;

namespace GF.DillyDally.Wpf.Client.Wpf
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
                return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue);
            }
        }

        public static void RegisterPackScheme()
        {
            if (!UriParser.IsKnownScheme(System.IO.Packaging.PackUriHelper.UriSchemePack))
            {
                UriParser.Register(new GenericUriParser
                    (GenericUriParserOptions.GenericAuthority), System.IO.Packaging.PackUriHelper.UriSchemePack, -1);
            }
        }
    }
}