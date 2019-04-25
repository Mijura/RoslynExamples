using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Linq;

namespace SymbolApiExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //workspace api
            var work = MSBuildWorkspace.Create();
            var solution = work.OpenSolutionAsync(@"..\..\..\..\RoslynExamples.sln").Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == "SymbolApiExample");
            if (project == null)
                throw new Exception("Could not find the SymbolApiExample project.");
            var compilation = project.GetCompilationAsync().Result;

            var b = compilation.GlobalNamespace;

        }
    }
}
