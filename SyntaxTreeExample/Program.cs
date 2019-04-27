using CodeGenerator;
using Microsoft.CodeAnalysis.CSharp;
using RoslynExamples;
using System;
using System.IO;

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
            var hello = new MladenWorld();
            hello.PrintHello();
        }
    }
    [DuplicateWithSuffix("World")]
    public class Mladen {
        public void PrintHello() {
            Console.WriteLine("\n\nHello world!");
        }
    }

    
}
