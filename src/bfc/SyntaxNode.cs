using System;

namespace bfc
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }
    }
}
