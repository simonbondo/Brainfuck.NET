using System;

namespace bfc
{
    public class SyntaxToken
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            this.Kind = kind;
            this.Position = position;
            this.Text = text;
            this.Value = value;
        }

        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
    }
}
