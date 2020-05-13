using System;

namespace bfc
{
    public class Lexer
    {
        private readonly string text;
        private int position;

        public Lexer(string text)
        {
            this.text = text;
        }

        private char Current
        {
            get
            {
                if (this.position >= this.text.Length)
                    return '\0';

                return this.text[this.position];
            }
        }

        private void Next()
        {
            this.position++;
        }

        public SyntaxToken NextToken()
        {
            if (this.position >= this.text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, this.position, "\0", default);

            SyntaxToken ReadContinuousToken(SyntaxKind kind, Func<char, bool> matcher, bool hasValue)
            {
                var start = this.position;

                while (matcher(this.Current))
                    this.Next();

                var length = this.position - start;
                var fragment = this.text.Substring(start, length);
                return new SyntaxToken(kind, start, fragment, hasValue ? (object)length : default);
            }

            if (char.IsWhiteSpace(this.Current))
                return ReadContinuousToken(SyntaxKind.WhiteSpaceToken, char.IsWhiteSpace, hasValue: false);

            if (this.Current == '>')
                return ReadContinuousToken(SyntaxKind.PointerRightToken, c => c == '>', hasValue: true);

            if (this.Current == '<')
                return ReadContinuousToken(SyntaxKind.PointerLeftToken, c => c == '<', hasValue: true);

            if (this.Current == '+')
                return ReadContinuousToken(SyntaxKind.IncrementToken, c => c == '+', hasValue: true);

            if (this.Current == '-')
                return ReadContinuousToken(SyntaxKind.DecrementToken, c => c == '-', hasValue: true);

            if (this.Current == '.')
                return new SyntaxToken(SyntaxKind.OutputToken, this.position++, ".", default);

            if (this.Current == ',')
                return new SyntaxToken(SyntaxKind.InputToken, this.position++, ",", default);

            if (this.Current == '[')
                return new SyntaxToken(SyntaxKind.OpenBracketToken, this.position++, "[", default);

            if (this.Current == ']')
                return new SyntaxToken(SyntaxKind.CloseBracketToken, this.position++, "]", default);

            // TODO: Maybe it would be simpler to just treat any non-instruction as WhiteSpaceTokens (exactly how the language is designed)
            if (char.IsLetterOrDigit(this.Current))
                return ReadContinuousToken(SyntaxKind.TextToken, char.IsLetterOrDigit, hasValue: false);

            if (char.IsPunctuation(this.Current) || char.IsSymbol(this.Current))
                return new SyntaxToken(SyntaxKind.SymbolToken, this.position++, this.text.Substring(this.position - 1, 1), default);

            return new SyntaxToken(SyntaxKind.BadToken, this.position++, this.text.Substring(this.position - 1, 1), default);
        }
    }
}
