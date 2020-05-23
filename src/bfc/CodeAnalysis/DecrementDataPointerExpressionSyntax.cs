using System.Collections.Generic;

namespace Brainfuck.CodeAnalysis
{
    public sealed class DecrementDataPointerExpressionSyntax : ExpressionSyntax
    {
        public DecrementDataPointerExpressionSyntax(SyntaxToken lessThanToken)
        {
            LessThanToken = lessThanToken;
        }

        public override SyntaxKind Kind => SyntaxKind.DecrementDataPointerExpression;
        public SyntaxToken LessThanToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.LessThanToken;
        }
    }
}
