using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Reflection;

/**
 * Jason Bock's example
 * 
 */
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var code =
@"using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine(""Hello compiled world!"");
        }
    }
}";
            //parse code using ParseSyntaxTree
            var tree = SyntaxFactory.ParseSyntaxTree(code);

            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            //compile that tree using CSharpCompilation
            var compilation = CSharpCompilation.Create(
                "HelloWorldCompiled.exe",
                options: new CSharpCompilationOptions(OutputKind.ConsoleApplication),   //should produce console application
                syntaxTrees: new[] { tree },
                references: new[] {
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Private.CoreLib.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Console.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.Extensions.dll")) 
                }
                    
            );

            //emit the assembly, assembly will not written to disk 'cause we are using memory stream
            using (var stream = new MemoryStream()) {
                var compileResult = compilation.Emit(stream);
                var assembly = Assembly.Load(stream.GetBuffer());
                // access and invoke main method
                assembly.EntryPoint.Invoke(null, BindingFlags.NonPublic | BindingFlags.Static, null, new object[] { null }, null);
            }
        }
    }
}
