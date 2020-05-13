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

            SyntaxToken ReadAsToken(SyntaxKind kind, Func<char, bool> matcher)
            {
                var start = this.position;

                while (matcher(this.Current))
                    this.Next();

                var length = this.position - start;
                var fragment = this.text.Substring(start, length);
                return new SyntaxToken(kind, start, fragment, length);
            }

            if (char.IsWhiteSpace(this.Current))
                return ReadAsToken(SyntaxKind.WhiteSpaceToken, char.IsWhiteSpace);

            // TODO: Not sure if the actual instructions should be concatenated here in the lexer or at a later stage (parser)
            if (this.Current == '>')
                return ReadAsToken(SyntaxKind.PointerRightToken, c => c == '>');

            if (this.Current == '<')
                return ReadAsToken(SyntaxKind.PointerLeftToken, c => c == '<');

            if (this.Current == '+')
                return ReadAsToken(SyntaxKind.IncrementToken, c => c == '+');

            if (this.Current == '-')
                return ReadAsToken(SyntaxKind.DecrementToken, c => c == '-');

            if (this.Current == '.')
                return new SyntaxToken(SyntaxKind.OutputToken, this.position++, ".", default);

            if (this.Current == ',')
                return new SyntaxToken(SyntaxKind.InputToken, this.position++, ",", default);

            if (this.Current == '[')
                return new SyntaxToken(SyntaxKind.OpenBracketToken, this.position++, "[", default);

            if (this.Current == ']')
                return new SyntaxToken(SyntaxKind.CloseBracketToken, this.position++, "]", default);

            if (char.IsLetterOrDigit(this.Current))
                return ReadAsToken(SyntaxKind.TextToken, char.IsLetterOrDigit);

            if (char.IsPunctuation(this.Current) || char.IsSymbol(this.Current))
                return new SyntaxToken(SyntaxKind.SymbolToken, this.position++, this.text.Substring(this.position - 1, 1), default);

            return new SyntaxToken(SyntaxKind.BadToken, this.position++, this.text.Substring(this.position - 1, 1), default);
        }
    }
}
