using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTreeExample
{
    public class IfRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitIfStatement(IfStatementSyntax node)
        {
            var body = node.Statement;

            var block = SyntaxFactory.Block(body);
            var newIfStatement = node.WithStatement(block);
            return newIfStatement;
        }
    }
}
