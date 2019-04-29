using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyntaxTreeExample
{
    public class TokenWalker : CSharpSyntaxWalker
    {

        public StringBuilder sb = new StringBuilder();

        public TokenWalker() : base(SyntaxWalkerDepth.Token)
        {
        }

        public override void VisitToken(SyntaxToken token)
        {
            sb.Append(token.ToFullString());
            base.VisitToken(token);
        }
    }
}
