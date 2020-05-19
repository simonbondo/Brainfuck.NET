using System;
using System.Linq;

namespace bfc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                //UseLexer(line);
                UseParser(line);
            }
        }

        private static void UseLexer(string line)
        {
            var lexer = new Lexer(line);

            while (true)
            {
                var token = lexer.NextToken();
                if (token.Kind == SyntaxKind.EndOfFileToken)
                    break;

                Console.Write($"{token.Kind}: '{token.Text}'");
                if (token.Value != null)
                    Console.Write($" {token.Value}");

                Console.WriteLine();
            }
        }

        private static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└── " : "├── ";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken token && token.Value != null)
            {
                Console.Write(" ");
                Console.Write(token.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }

        private static void UseParser(string line)
        {
            var originalColor = Console.ForegroundColor;

            var parser = new Parser(line);
            var syntaxTree = parser.Parse();

            if (syntaxTree.Root != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                PrettyPrint(syntaxTree.Root);
                Console.ForegroundColor = originalColor;
            }

            if (!parser.Diagnostics.Any())
            {
                var evaluator = new Evaluator(syntaxTree.Root);
                var result = evaluator.Evaluate();
                var memory = evaluator.GetMemory();
                if (memory.Length > 0)
                {
                    Console.WriteLine($"Memory bytes ({memory.Length}): [ {string.Join(',', memory)} ]");
                    Console.WriteLine($"Memory chars ({memory.Length}): [ {string.Join(',', memory.Select(b => (char)b))} ]");
                }
                Console.WriteLine(result);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                foreach (var diagnostic in syntaxTree.Diagnostics)
                    Console.WriteLine(diagnostic);
                Console.ForegroundColor = originalColor;
            }
        }
    }
}
