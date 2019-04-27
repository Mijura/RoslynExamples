using CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynExamples;
using System;
using System.IO;
using System.Linq;

namespace SyntaxTreeExample
{
    /**
     * Syntax Tree API examples.
     */
    class Program
    {

        static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText("../../../Program.cs"));
            TreeWalker treeWalker = new TreeWalker();
            //visit and display syntax tree
            treeWalker.Visit(tree.GetRoot());

            // use class created in design time
            var hello = new MladenWorld();
            hello.PrintHello();

            tree = CSharpSyntaxTree.ParseText(@"class{}");

            //get first error (also we could get warnings)
            var error = tree.GetDiagnostics().Where(n => n.Severity == DiagnosticSeverity.Error).First();
            Console.WriteLine(error.ToString());

        }
    }
    [DuplicateWithSuffix("World")]
    public class Mladen {
        public void PrintHello() {
            Console.WriteLine("\n\nHello world!");
        }
    }

    
}
