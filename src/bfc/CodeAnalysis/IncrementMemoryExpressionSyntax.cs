using System.Collections.Generic;

namespace Brainfuck.CodeAnalysis
{
    public sealed class IncrementMemoryExpressionSyntax : ExpressionSyntax
    {
        public IncrementMemoryExpressionSyntax(SyntaxToken plusToken)
        {
            this.PlusToken = plusToken;
        }

        public override SyntaxKind Kind => SyntaxKind.IncrementMemoryExpression;
        public SyntaxToken PlusToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.PlusToken;
        }
    }
}
