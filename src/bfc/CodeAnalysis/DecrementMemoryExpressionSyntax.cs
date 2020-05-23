using System.Collections.Generic;

namespace Brainfuck.CodeAnalysis
{
    public sealed class DecrementMemoryExpressionSyntax : ExpressionSyntax
    {
        public DecrementMemoryExpressionSyntax(SyntaxToken minusToken)
        {
            MinusToken = minusToken;
        }

        public override SyntaxKind Kind => SyntaxKind.DecrementMemoryExpression;
        public SyntaxToken MinusToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.MinusToken;
        }
    }
}
