using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;

namespace RoslynExamples
{
    public class TreeWalker: CSharpSyntaxWalker
    {
        public TreeWalker() : base(SyntaxWalkerDepth.StructuredTrivia)
        {
        }

        public override void Visit(SyntaxNode node)
        {
            Console.WriteLine(node.Kind());
            base.Visit(node);
        }

        public override void VisitToken(SyntaxToken token)
        {
            Console.WriteLine(String.Format("{0} {1}", token.Kind(), token));
            base.VisitToken(token);
        }
    }
}
