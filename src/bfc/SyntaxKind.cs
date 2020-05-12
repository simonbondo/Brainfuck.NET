using System;

namespace bfc
{
    public enum SyntaxKind
    {
        PointerRightToken,
        PointerLeftToken,
        IncrementToken,
        DecrementToken,
        OutputToken,
        InputToken,
        OpenBracketToken,
        CloseBracketToken,
        WhiteSpaceToken,
        TextToken,
        SymbolToken,
        EndOfFileToken,
        BadToken
    }
}
