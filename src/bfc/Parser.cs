using System;
using System.Collections.Generic;

namespace bfc
{
    public class Parser
    {
        private readonly SyntaxToken[] tokens;
        private int position;

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();

            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();

                if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadToken)
                    tokens.Add(token);
            } while (token.Kind != SyntaxKind.EndOfFileToken);

            this.tokens = tokens.ToArray();
        }

        private SyntaxToken Peek(int offset)
        {
            var index = this.position + offset;
            if (index >= this.tokens.Length)
                return this.tokens[this.tokens.Length - 1];

            return this.tokens[index];
        }

        private SyntaxToken Current => this.Peek(0);

        private SyntaxToken NextToken()
        {
            var current = this.Current;
            this.position++;
            return current;
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (this.Current.Kind == kind)
                return this.NextToken();

            return new SyntaxToken(kind, this.Current.Position, null, null);
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            var plusToken = this.Match(SyntaxKind.PlusToken);
            return new IncrementDataPointerExpressionSyntax(plusToken);
        }

        public ExpressionSyntax Parse()
        {
            var primaryExpression = this.ParsePrimaryExpression();
            return primaryExpression;
        }
    }
}
