using System;
using System.Configuration;
using System.Linq;
using System.Patterns.Caching;

namespace HLLMRKLINK.Utility
{
    public static class GnuPgpSettings
    {
        static GnuPgpSettings()
        {
            var appSettings = ConfigurationManager.AppSettings;
            if ((GnuPgpPath = appSettings["GnuPgpPath"]) == null)
                throw new NullReferenceException("AppSettings::GnuPgpPath");
            if ((GnuPgpPassphase = appSettings["GnuPgpPassphase"]) == null)
                throw new NullReferenceException("AppSettings::GnuPgpPassphase");
        }

        public static string GnuPgpPath { get; private set; }
        public static string GnuPgpPassphase { get; private set; }
    }
}