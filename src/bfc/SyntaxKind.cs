using System;

namespace Brainfuck
{
    public enum SyntaxKind
    {
        GreaterThanToken,
        LessThanToken,
        PlusToken,
        MinusToken,
        PeriodToken,
        CommaToken,
        OpenBracketToken,
        CloseBracketToken,
        WhiteSpaceToken,
        LetterOrDigitToken,
        PunctuationOrSymbolToken,
        EndOfFileToken,
        BadToken,
        IncrementDataPointerExpression,
        IncrementMemoryExpression
    }
}
