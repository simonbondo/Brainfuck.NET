using System;

namespace bfc
{
    public sealed class IncrementDataPointerExpressionSyntax : ExpressionSyntax
    {
        public IncrementDataPointerExpressionSyntax(SyntaxToken greaterThanToken)
        {
            this.GreaterThanToken = greaterThanToken;
        }

        public override SyntaxKind Kind => SyntaxKind.IncrementDataPointerExpression;
        public SyntaxToken GreaterThanToken { get; }
    }
}
