using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxTreeExample
{
    public class SemicolonRemovalExample : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
        {
            return node.WithSemicolonToken(SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken)
                .WithLeadingTrivia(node.SemicolonToken.LeadingTrivia)
                .WithTrailingTrivia(node.SemicolonToken.TrailingTrivia));
        }
    }
}
