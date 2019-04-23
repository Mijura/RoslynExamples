using CodeGenerator;
using Microsoft.CodeAnalysis.CSharp;
using RoslynExamples;
using System;
using System.IO;

namespace SyntaxTreeExample
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText("../../../Program.cs"));
            TreeWalker treeWalker = new TreeWalker();
            //visit and display syntax tree
            treeWalker.Visit(tree.GetRoot());
            var hello = new HelloWorld();
            hello.printHello();
        }
    }
    [DuplicateWithSuffix("World")]
    public class Hello {
        public void printHello() {
            Console.WriteLine("Hello world");
        }
    }

    
}
