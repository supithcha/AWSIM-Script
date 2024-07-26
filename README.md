# AWSIM-Script
A script language for Autoware AWSIM-Labs

## ANTLR
check out it here: https://www.antlr.org/

Some good tutorials I found:

https://putridparrot.com/blog/antlr-in-c/ (recommended since it provides detailed code)

https://tomassetti.me/getting-started-with-antlr-in-csharp/

## Generate code from grammar file (.b4) file
If you curious how did I generate C# code (in `generated_code` folder), check out these:

https://github.com/antlr/antlr4/blob/master/doc/csharp-target.md

https://github.com/antlr/antlr4/tree/master/runtime/CSharp/src

## ANTLR grammar
The syntax used in grammar file is close to BNF, you can quickly check it by Google. Wiki also does a good job:

https://en.wikipedia.org/wiki/Backus%E2%80%93Naur_form


## Follow
- First, check out the example input file `inputs/input.txt`.

- Understand the crafted grammar in `grammar/AWSIMScriptGrammar.g4` file. Note that it likely the case that we need to update this grammar later.

- Understand basic algorithm to visit tree (listener, visitor).

- Start implementing