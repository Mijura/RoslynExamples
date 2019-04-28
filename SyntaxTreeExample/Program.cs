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
            var repeat = true;
            while (repeat) {
                Console.WriteLine("1 - Display Syntax Tree Example");
                Console.WriteLine("2 - Class Created in Design Time Example");
                Console.WriteLine("3 - Display First Error Example");
                Console.WriteLine("0 - Exit");
                Console.Write("\nChoose option: ");

                var option = int.Parse(Console.ReadLine());

                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        DisplaySintaxTreeExample();
                        break;
                    case 2:
                        UseClassCreatedInDesignTimeExample();
                        break;
                    case 3:
                        DisplayFirstErrorExample();
                        break;
                    case 0:
                        repeat = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }

                Console.WriteLine();
                
            }
            
            

        }

        private static void DisplayFirstErrorExample()
        {
            var tree = CSharpSyntaxTree.ParseText(@"class{}");

            //get first error (also we could get warnings)
            var error = tree.GetDiagnostics().Where(n => n.Severity == DiagnosticSeverity.Error).First();
            Console.WriteLine(error.ToString());
        }

        private static void UseClassCreatedInDesignTimeExample()
        {
            // use class created in design time
            var hello = new MladenWorld();
            hello.PrintHello();
        }

        private static void DisplaySintaxTreeExample()
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText("../../../Program.cs"));
            TreeWalker treeWalker = new TreeWalker();
            //visit and display syntax tree
            treeWalker.Visit(tree.GetRoot());
        }
    }
    [DuplicateWithSuffix("World")]
    public class Mladen {
        public void PrintHello() {
            Console.WriteLine("Hello world!");
        }
    }

    
}
