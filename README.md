# Brainfuck.NET

![.NET 5](https://github.com/simonbondo/Brainfuck.NET/workflows/.NET%205/badge.svg)

This is a compiler for [Brainfuck](https://en.wikipedia.org/wiki/Brainfuck) written in C# targeting .NET 5. The eventual goal is to compile Brainfuck source into valid IL code, which can be executed by the .NET runtime.

I felt inspired while following the [Compiler from scratch](http://minsk-compiler.net/) adventures by [@terrajobst](https://twitter.com/terrajobst), where he creates a compiler for the made-up language [Minsk](https://github.com/terrajobst/Minsk). I wanted to see if I could apply some of the same lessons to an [esoteric programming language](https://en.wikipedia.org/wiki/Esoteric_programming_language) like Brainfuck.

## How Brainfuck works

Brainfuck consists of an instruction pointer, a data pointer, some amount of memory and 8 simple instructions.

The instruction pointer starts at *0* and advances forward by *1* after executing each instruction. After the last instruction, the program terminates.

Memory is an array of bytes that is all initialized at zero. The data pointer starts by pointing to the first address. Memory can then be manipulated at the current address and the pointer can be moved using the instructions.

The 8 instructions are as follows:

instruction | description
------------|-------------
`>`         | Increment the data pointer by one (move it to the right).
`<`         | Decrement the data pointer by one (move it to the left).
`+`         | Increment the value of the memory at current location by one.
`-`         | Decrement the value of the memory at current location by one.
`.`         | Output the value at the current location.
`,`         | Read one byte of input and replace the value at current location.
`[`         | Jump forward to matching `]`, if value at current location is `0`.
`]`         | Jump backwards to matching `[`, if value at current location is *not* `0`.

To be a valid program, the number of `[` and `]` should be balanced, meaning they always come in pairs. Nesting is allowed.

Any character that is not in the list of valid instructions should simply be ignored, similar to how whitespace is treated in most regular programming languages.
