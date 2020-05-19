using System;
using System.Linq;
using Brainfuck.CodeAnalysis;

namespace Brainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            var originalColor = Console.ForegroundColor;
            var showParseTree = false;

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "#showTree")
                {
                    showParseTree = !showParseTree;
                    Console.WriteLine(showParseTree ? "Showing parse trees." : "Not showing parse trees.");
                    continue;
                }
                else if (line == "#cls")
                {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                if (showParseTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = originalColor;
                }

                if (!syntaxTree.Diagnostics.Any())
                {
                    var evaluator = new Evaluator(syntaxTree.Root);
                    var result = evaluator.Evaluate();
                    var memory = evaluator.GetMemory();
                    if (memory.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"Memory bytes ({memory.Length}): [ {string.Join(',', memory)} ]");
                        Console.WriteLine($"Memory chars ({memory.Length}): [ {string.Join(',', memory.Select(b => (char)b))} ]");
                        Console.ForegroundColor = originalColor;
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

        private static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            if (node is null)
                return;

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

        }
    }
}
