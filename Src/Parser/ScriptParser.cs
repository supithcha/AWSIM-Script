using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using AWSIM_Script.Object;

namespace AWSIM_Script.Parser
{
	public class ScriptParser
	{
		public ScriptParser()
		{
		}
        /// <summary>
        /// provide the prased tree, convert it to a Scenario object
        /// </summary>
        /// <param name="antlrTree"></param>
        /// <returns></returns>
        public Scenario ParseScript(IParseTree antlrTree)
        {
            return new Scenario();
        }

        public static void Main()
        {
            // This is an example showing how we can use Anltr to obtain the parsed Tree
            String input = File.ReadAllText("inputs/input.txt");
            ICharStream stream = CharStreams.fromString(input);
            ITokenSource lexer = new AWSIMScriptGrammarLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            AWSIMScriptGrammarParser parser = new AWSIMScriptGrammarParser(tokens);
            IParseTree tree = parser.scenario();
            Console.Write(tree.ToStringTree());
        }
    }
}

