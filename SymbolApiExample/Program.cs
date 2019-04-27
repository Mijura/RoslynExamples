using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Linq;

namespace SymbolApiExample
{
    /**
     * Symbol API (Workspace API) examples.
     */
    class Program
    {
        static void Main(string[] args)
        {
            MSBuildLocator.RegisterDefaults();

            var work = MSBuildWorkspace.Create();
            var solution = work.OpenSolutionAsync(@"..\..\..\..\RoslynExamples.sln").Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == "SyntaxTreeExample");
            if (project == null)
                throw new Exception("Could not find the SyntaxTreeExample project.");
            var compilation = project.GetCompilationAsync().Result;
            ReviewSymbolTable(compilation);
        }

        private static void ReviewSymbolTable(Compilation compilation)
        {
            foreach (var member in compilation.Assembly.GlobalNamespace.GetMembers()
                .Where(member => member.CanBeReferencedByName))
            {
                Console.WriteLine(member.Name);
                foreach (var item in member.GetTypeMembers()
                    .Where(item => item.CanBeReferencedByName))
                {
                    Console.WriteLine("\t{0}:{1}", item.TypeKind, item.Name);
                    foreach (var innerItem in item.GetMembers()
                        .Where(innerItem => innerItem.CanBeReferencedByName))
                    {
                        Console.WriteLine("\t\t{0}:{1}", innerItem.Kind, innerItem.Name);
                    }
                }
            }
        }
    }
}
