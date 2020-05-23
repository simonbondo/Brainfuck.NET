using System;
using System.Collections.Immutable;

namespace Brainfuck.CodeAnalysis
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

            if (node is DecrementMemoryExpressionSyntax decMemory)
            {
                // TODO: Currently, if memory is 0, the new value becomes 255. Is wrapping like this allowed in BF?
                this.memory[this.dataPointer] -= (byte)decMemory.MinusToken.Value;
                return this.memory[this.dataPointer];
            }

            if (node is IncrementDataPointerExpressionSyntax incPointer)
            {
                var targetDataPointer = this.dataPointer + (int)incPointer.GreaterThanToken.Value;
                if (targetDataPointer >= this.memoryLength)
                    throw new Exception($"Data pointer trying to move outside memory bounds. Current: {this.dataPointer}, Target: {targetDataPointer}, Memory Bounds: 0-{this.memoryLength}");

                this.dataPointer = targetDataPointer;
                return this.memory[this.dataPointer];
            }

            if (node is DecrementDataPointerExpressionSyntax decPointer)
            {
                var targetDataPointer = this.dataPointer - (int)decPointer.LessThanToken.Value;
                if (targetDataPointer < 0)
                    throw new Exception($"Data pointer trying to move outside memory bounds. Current: {this.dataPointer}, Target: {targetDataPointer}, Memory Bounds: 0-{this.memoryLength}");

                this.dataPointer = targetDataPointer;
                return this.memory[this.dataPointer];
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
