using System;
using System.Diagnostics;
using System.IO;

namespace HLLMRKLINK.Utility
{
    public class GnuPgp
    {
        private const string DecryptArgumentsABCDE = "--homedir \"{0}\" --passphrase \"{1}\" --yes --always-trust --output \"{2}\" --decrypt \"{3}\"";
        private const string EncryptArgumentsABCDE = "--homedir \"{0}\" --passphrase \"{1}\" --yes --always-trust --recipient \"{2}\" --output \"{3}\" --encrypt \"{4}\"";
        private const string ImportArgumentsABC = "--homedir \"{0}\" --passphrase \"{1}\" --yes --import \"{2}\"";

        static GnuPgp()
        {
            if (!Directory.Exists(HomeDirectory))
                Directory.CreateDirectory(HomeDirectory);
        }

        public static void Encrypt(string recipient, string inputFilePath, string outputFilePath)
        {
            var arguments = string.Format(EncryptArgumentsABCDE, HomeDirectory, GnuPgpSettings.GnuPgpPassphase, recipient, outputFilePath, inputFilePath);
            var process = Process.Start(new ProcessStartInfo(Executable)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = arguments,
            });
            if (process != null)
                process.WaitForExit(60000);
        }

        public void Decrypt(string inputFilePath, string outputFilePath)
        {
            string arguments = string.Format(DecryptArgumentsABCDE, HomeDirectory, GnuPgpSettings.GnuPgpPassphase, outputFilePath, inputFilePath);
            var process = Process.Start(new ProcessStartInfo(Executable)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = arguments,
            });
            if (process != null)
                process.WaitForExit(60000);
        }

        public void Import(string keyFilePath)
        {
            string arguments = string.Format(ImportArgumentsABC, HomeDirectory, GnuPgpSettings.GnuPgpPassphase, keyFilePath);
            var process = Process.Start(new ProcessStartInfo(Executable)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = arguments,
            });
            if (process != null)
                process.WaitForExit(60000);
        }

        private static string Executable
        {
            get { return GnuPgpSettings.GnuPgpPath.EnsureEndsWith("\\") + "gpg.exe"; }
        }

        private static string HomeDirectory
        {
            get { return GnuPgpSettings.GnuPgpPath.EnsureEndsWith("\\") + "store"; }
        }
    }
}