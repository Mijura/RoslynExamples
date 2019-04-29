using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTreeExample
{
    public class EmptyStatementRemoval : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
        {
            // if we return null we will remove this statment from new tree
            return null;
        }
    }
}
