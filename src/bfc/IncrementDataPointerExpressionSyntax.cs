﻿using System;
using System.Collections.Generic;

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

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.GreaterThanToken;
        }
    }
}
