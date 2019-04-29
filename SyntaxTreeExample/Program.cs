using CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public static void Main(string[] args)
        {
            var repeat = true;
            while (repeat) {
                Console.WriteLine("1 - Display Syntax Tree Example");
                Console.WriteLine("2 - Class Created in Design Time Example");
                Console.WriteLine("3 - Display First Error Example");
                Console.WriteLine("4 - Display First Method");
                Console.WriteLine("5 - Change Method Return Type Example");
                Console.WriteLine("6 - Find The Class Name That Contains Method Example");
                Console.WriteLine("7 - Visit Only Classes And Methods Declaration Example");
                Console.WriteLine("8 - Visit Only Tokens Example");
                Console.WriteLine("9 - Empty Statement Removal Example");
                Console.WriteLine("10 - Semicolon Removal Example");
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
                    case 4:
                        DisplayFirstMethodExample();
                        break;
                    case 5:
                        ChangeMethodReturnTypeExample();
                        break;
                    case 6:
                        DisplayTypeThatContainsMethodExample();
                        break;
                    case 7:
                        VisitOnlyClassAndMethodsDeclarationsExample();
                        break;
                    case 8:
                        VisitOnlyTokensExample();
                        break;
                    case 9:
                        EmptyStatementRemovalExample();
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

        private static void EmptyStatementRemovalExample()
        {
            // A syntax tree with an unnecessary semicolon on its own line
             var tree = CSharpSyntaxTree.ParseText(@"
                public class Sample
                {
                    // Method prints empty line
                    public void Foo()
                    {
                        Console.WriteLine();
                        // comment 1
                        ; 
                        // comment 2
                    }
                }");

            var rewriter = new EmptyStatementRemoval();
            var result = rewriter.Visit(tree.GetRoot()); //this will create new root node!
            Console.WriteLine(result.ToFullString()); // comment 1 will be removed also!

        }

        private static void VisitOnlyTokensExample()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
                public class MyClass
                {
                    public void Main()
                    {
                    }
                }
            ");

            var walker = new TokenWalker();
            walker.Visit(tree.GetRoot());
            Console.WriteLine(walker.sb.ToString());
        }

        private static void VisitOnlyClassAndMethodsDeclarationsExample()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
                public class MyClass
                {
                    public void MyMethod()
                    {
                    }
                }
                public class MyOtherClass
                {
                    public void MyMethod(int n)
                    {
                    }
                }
            ");

            var walker = new ClassMethodWalker();
            walker.Visit(tree.GetRoot());
        }

        private static void DisplayTypeThatContainsMethodExample()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
                        public class MyClass
                        {
                            public void MyMethod()
                            {
                            }
                            public void MyMethod(int n)
                            {
                            }
                        }");

            var syntaxRoot = tree.GetRoot();

            //match first method with parameters
            var myMethod = syntaxRoot.DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where(m => m.ParameterList.Parameters.Any())
                .First();

            //find the type that contains this method
            var containingType = myMethod.Ancestors().OfType<TypeDeclarationSyntax>().First();
            Console.WriteLine("ContainingType: {0}", containingType.Identifier);
        }

        private static void ChangeMethodReturnTypeExample()
        {
            var tree = CSharpSyntaxTree.ParseText("class C{ void Method(){}}");
            var root = tree.GetRoot();
            var method = root.DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            var returnType = SyntaxFactory.ParseTypeName("string ");
            var newMethod = method.WithReturnType(returnType);
            Console.WriteLine(newMethod);
        }

        private static void DisplayFirstMethodExample()
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText("../../../Program.cs"));
            var method = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            Console.WriteLine("Method Name: {0}", method.Identifier);
            Console.WriteLine("Return Type: {0}", method.ReturnType);

            // display modifiers
            Console.Write("Modifiers: ");
            foreach (var modifier in method.Modifiers)
                Console.Write(" {0}", modifier.Text);
            Console.WriteLine();

            Console.WriteLine("Parameters: {0}", method.ParameterList);
            Console.WriteLine("Body: {0}", method.Body);
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
