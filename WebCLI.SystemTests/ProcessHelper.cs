using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using WebCLI.SystemTests.Code.Extensions;

namespace WebCLI.SystemTests
{
    internal class ProcessHelper
    {
        //#if DEBUG
        //        private const string webcliPath = @"WebCLI\bin\Debug\netcoreapp3.1\web.exe";
        //#else
        //        private const string webcliPath = @"WebCLI\bin\Release\netcoreapp3.1\web.exe";
        //#endif

#if DEBUG
        private const string webcliPath = @"WebCLI\bin\Debug\netcoreapp3.1\web.exe";
#else
        private const string webcliPath = @"release/linux-x64/web";
#endif

        internal static ProcessHelperOutput StartConsoleApplication(string args)
        {
            var workspace = new DirectoryInfo(AssemblyDirectory).Parent.Parent.Parent.Parent;
            var exepath = Path.Combine(workspace.FullName, webcliPath);

            Console.WriteLine(exepath);

            Process process = new Process();
            process.StartInfo.FileName = exepath;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            return new ProcessHelperOutput
            {
                Output = output.NormalizeLineEndings(),
                Error = error,
                ExitCode = (ExitCode)process.ExitCode
            };
        }

        private static string AssemblyDirectory
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();

                if (assembly == null)
                {
                    throw new NullReferenceException("Executing assembly not found.");
                }

                var codeBase = assembly.CodeBase;

                if (codeBase == null)
                {
                    throw new NullReferenceException("Executing assembly codebase not found.");
                }

                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                var directoryName = Path.GetDirectoryName(path);

                if (directoryName == null)
                {
                    throw new NullReferenceException("Directory name not found.");
                }

                return directoryName;
            }
        }
    }

    internal class ProcessHelperOutput
    {
        public ExitCode ExitCode { get; set; }
        public string Error { get; set; } = "";
        public string Output { get; set; } = "";
    }
}
