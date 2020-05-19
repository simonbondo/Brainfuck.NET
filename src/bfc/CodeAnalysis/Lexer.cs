using System;
using System.Collections.Generic;

namespace Brainfuck.CodeAnalysis
{
    public class Lexer
    {
        private readonly string text;
        private int position;
        private List<string> diagnostics = new List<string>();

        public Lexer(string text)
        {
            this.text = text;
        }

        public IEnumerable<string> Diagnostics => this.diagnostics;

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
                return new SyntaxToken(SyntaxKind.GreaterThanToken, this.position++, ">", 1);
                //return ReadContinuousToken(SyntaxKind.GreaterThanToken, c => c == '>', hasValue: true);

            if (this.Current == '<')
                return new SyntaxToken(SyntaxKind.LessThanToken, this.position++, "<", 1);
                //return ReadContinuousToken(SyntaxKind.LessThanToken, c => c == '<', hasValue: true);

            if (this.Current == '+')
                return new SyntaxToken(SyntaxKind.PlusToken, this.position++, "+", (byte)1);
                //return ReadContinuousToken(SyntaxKind.PlusToken, c => c == '+', hasValue: true);

            if (this.Current == '-')
                return new SyntaxToken(SyntaxKind.MinusToken, this.position++, "-", (byte)1);
                //return ReadContinuousToken(SyntaxKind.MinusToken, c => c == '-', hasValue: true);

            if (this.Current == '.')
                return new SyntaxToken(SyntaxKind.PeriodToken, this.position++, ".", default);

            if (this.Current == ',')
                return new SyntaxToken(SyntaxKind.CommaToken, this.position++, ",", default);

            if (this.Current == '[')
                return new SyntaxToken(SyntaxKind.OpenBracketToken, this.position++, "[", default);

            if (this.Current == ']')
                return new SyntaxToken(SyntaxKind.CloseBracketToken, this.position++, "]", default);

            // TODO: Maybe it would be simpler to just treat any non-instruction as WhiteSpaceTokens (exactly how the language is designed)
            //if (char.IsLetterOrDigit(this.Current))
            //    return ReadContinuousToken(SyntaxKind.LetterOrDigitToken, char.IsLetterOrDigit, hasValue: false);

            //if (char.IsPunctuation(this.Current) || char.IsSymbol(this.Current))
            //    return new SyntaxToken(SyntaxKind.PunctuationOrSymbolToken, this.position++, this.text.Substring(this.position - 1, 1), default);

            this.diagnostics.Add($"ERROR: Bad charater input: '{this.Current}'");
            return new SyntaxToken(SyntaxKind.BadToken, this.position++, this.text.Substring(this.position - 1, 1), default);
        }
    }
}
