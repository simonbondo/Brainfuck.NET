using System;
using System.Collections.Generic;

namespace Brainfuck
{
    public class Parser
    {
        private readonly SyntaxToken[] tokens;

        private List<string> diagnostics = new List<string>();
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
            this.diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => this.diagnostics;

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

            this.diagnostics.Add($"ERROR: Unexpected token <{this.Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, this.Current.Position, null, null);
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (this.Current.Kind == SyntaxKind.GreaterThanToken)
            {
                var greaterThanToken = this.Match(SyntaxKind.GreaterThanToken);
                return new IncrementDataPointerExpressionSyntax(greaterThanToken);
            }
            if (this.Current.Kind == SyntaxKind.PlusToken)
            {
                var plusToken = this.Match(SyntaxKind.PlusToken);
                return new IncrementMemoryExpressionSyntax(plusToken);
            }

            this.diagnostics.Add($"ERROR: Unknown token <{this.Current.Kind}>");
            return null;
        }

        private ExpressionSyntax ParseExpression()
        {
            return this.ParsePrimaryExpression();
        }

        public SyntaxTree Parse()
        {
            var expression = this.ParseExpression();
            var endOfFileToken = this.Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(this.diagnostics, expression, endOfFileToken);
        }
    }
}
