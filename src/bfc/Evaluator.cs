using System;
using System.Collections.Immutable;

namespace bfc
{
    public class Evaluator
    {
        private readonly ExpressionSyntax root;
        private readonly int memoryLength;
        private readonly byte[] memory;

        private int dataPointer;

        public Evaluator(ExpressionSyntax root, int memoryLength = 8)
        {
            this.root = root;
            // TODO: Dynamic memory length?
            this.memoryLength = memoryLength < 0 ? 0 : memoryLength;
            this.memory = new byte[this.memoryLength];
            this.dataPointer = 0;
        }

        public ImmutableArray<byte> GetMemory() => this.memory.ToImmutableArray();

        public byte Evaluate()
        {
            return EvaluateExpression(this.root);
        }

        private byte EvaluateExpression(ExpressionSyntax node)
        {
            if (node is IncrementMemoryExpressionSyntax incMemory)
            {
                this.memory[this.dataPointer] += (byte)incMemory.PlusToken.Value;
                return this.memory[this.dataPointer];
            }

            if (node is IncrementDataPointerExpressionSyntax incPointer)
            {
                var targetDataPointer = this.dataPointer + (int)incPointer.GreaterThanToken.Value;
                if (targetDataPointer >= this.memoryLength)
                    throw new Exception($"Data pointer trying to move outside memory bounds. Current: {this.dataPointer}, Target: {targetDataPointer}, Memory Bounds: {this.memoryLength}");

                this.dataPointer = targetDataPointer;
                return this.memory[this.dataPointer];
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
