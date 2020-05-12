# Brainfuck.NET

This is a compiler for [Brainfuck](https://en.wikipedia.org/wiki/Brainfuck) using .NET written in C#. The eventual goal is to compile Brainfuck source into valid IL code, which can be executed by the dotnet runtime.

I felt inspired while following the [Compiler from scratch](https://www.youtube.com/playlist?list=PLRAdsfhKI4OWNOSfS7EUu5GRAVmze1t2y) adventures by [@terrajobst](https://twitter.com/terrajobst), and wanted to see if I could apply some of the same lessons to an [esoteric programming language](https://en.wikipedia.org/wiki/Esoteric_programming_language) like Brainfuck.

## Language design

Brainfuck consists of an instruction pointer, a data pointer, memory and 8 simple instructions.

The instruction pointer starts at `0` and moves forward by `1` after each instruction (with exceptions). After the last instruction, the program terminates.

Memory is an array of bytes that is initialized with all `0`. The data pointer starts by pointing at the first address. Memory can then be manipulated at the current address and the pointer can be moved using the instructions.

The 8 instructions are as follows:

instruction | description
------------|-------------
`>`         | Increment the data pointer by one.
`<`         | Decrement the data pointer by one.
`+`         | Increment the value of the memory at current location by one.
`-`         | Decrement the value of the memory at current location by one.
`.`         | Output the value at the current location.
`,`         | Read one byte of input and store the value at current location.
`[`         | Jump forward to matching `]`, if value at current location is `0`
`]`         | Jump backwards to matching `[`, if value at current location is *not* `0`

To be a valid program, `[` and `]` should always match as a pair. Nesting is also valid.

Any charactor that is not in the list of valid instructions should simply be ignored, similar to whitespace in most regular programming languages.
