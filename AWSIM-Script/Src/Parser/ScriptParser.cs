using System;
using System.Text.RegularExpressions;
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
        /// 
        public Scenario ParseScript(IParseTree antlrTree)
        {
            return new Scenario();
        }


        //public Scenario ParseScript(string input)
        //{ return new Scenario(); }

        // TODO 0: Complete ParseScript function to convert prased tree to a Scenario object
        public Scenario ParseScript(string input)
        {
            var scenario = new Scenario();
            var variableMap = new Dictionary<string, string>();

            ICharStream stream = CharStreams.fromString(input);
            ITokenSource lexer = new AWSIMScriptGrammarLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            AWSIMScriptGrammarParser parser = new AWSIMScriptGrammarParser(tokens);
            AWSIMScriptGrammarParser.ScenarioContext tree = parser.scenario();

            foreach (var statement in tree.statement())
            {
                if (statement.assignmentStm() != null)
                {
                    var assignmentStmContext = statement.assignmentStm();
                    var variableName = assignmentStmContext.variableExp().GetText();
                    var expression = assignmentStmContext.expression().GetText().Trim('"');

                    variableMap[variableName] = expression;

                    if (variableName.StartsWith("npc"))
                    {
                        var npc = ParseNPC(assignmentStmContext.expression().function(), variableMap);
                        scenario.NPCs.Add(npc);
                    }
                }
                else if (statement.function() != null)
                {
                    var functionContext = statement.function();
                    var functionName = functionContext.ID().GetText();
                    if (functionName == "NPC")
                    {
                        var npc = ParseNPC(functionContext, variableMap);
                        scenario.NPCs.Add(npc);
                    }
                }
            }

            return scenario;
        }

        //private NPCScriptObject ParseNPC(AWSIMScriptGrammarParser.FunctionContext context, Dictionary<string, string> variableMap)
        //{
        //    var npc = new NPCScriptObject
        //    {
        //        Config = new NPCConfig
        //        {
        //            RouteSpeeds = new Dictionary<string, float>()
        //        }
        //    };
        //    var args = context.argumentList().expression();

        //    for (int i = 0; i < args.Length; i++)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Argument {i}: {args[i]?.GetText()}");
        //    }

        //    string vehicleTypeString;
        //    if (args[0].variableExp() != null)
        //    {
        //        if (!variableMap.TryGetValue(args[0].GetText(), out vehicleTypeString))
        //        {
        //            throw new ArgumentException($"Variable {args[0].GetText()} not found in variable map");
        //        }
        //    }
        //    else
        //    {
        //        vehicleTypeString = args[0].GetText().Trim('"');
        //    }

        //    System.Diagnostics.Debug.WriteLine($"Vehicle Type String: {vehicleTypeString}");

        //    if (!Enum.TryParse(vehicleTypeString, true, out VehicleType vehicleType))
        //    {
        //        throw new ArgumentException($"Invalid vehicle type: {vehicleTypeString}");
        //    }

        //    System.Diagnostics.Debug.WriteLine($"Parsed Vehicle Type: {vehicleType}");
        //    npc.VehicleType = vehicleType;


        //    npc.InitialPosition = ParseLanePosition(args[1], variableMap);
        //    npc.Goal = ParseLanePosition(args[2], variableMap);

        //    if (args.Length > 3)
        //    {
        //        string routesText;
        //        if (args[3].variableExp() != null)
        //        {
        //            if (!variableMap.TryGetValue(args[3].GetText(), out routesText))
        //            {
        //                throw new ArgumentException($"Variable {args[3].GetText()} not found in variable map");
        //            }
        //        }
        //        else
        //        {
        //            routesText = args[3].GetText().Trim('"');
        //        }

        //        var routes = routesText.Trim('[', ']', '"').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //        foreach (var route in routes)
        //        {
        //            var parts = route.Split(new[] { "with-speed-limit" }, StringSplitOptions.None);
        //            var lane = parts[0].Trim().Trim('"'); // Added Trim('"') to handle extra quotes
        //            float speed = parts.Length > 1 ? float.Parse(parts[1].Trim()) : 0;

        //            System.Diagnostics.Debug.WriteLine($"Route: {lane}, Speed: {speed}");

        //            npc.Config.RouteSpeeds[lane] = speed;
        //        }
        //    }

        //    return npc;
        //}

        private NPCScriptObject ParseNPC(AWSIMScriptGrammarParser.FunctionContext context, Dictionary<string, string> variableMap)
        {
            var npc = new NPCScriptObject
            {
                Config = new NPCConfig
                {
                    RouteSpeeds = new Dictionary<string, float>()
                }
            };
            var args = context.argumentList().expression();

            for (int i = 0; i < args.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine($"Argument {i}: {args[i]?.GetText()}");
            }

            string vehicleTypeString;
            if (args[0].variableExp() != null)
            {
                if (!variableMap.TryGetValue(args[0].GetText(), out vehicleTypeString))
                {
                    throw new ArgumentException($"Variable {args[0].GetText()} not found in variable map");
                }
            }
            else
            {
                vehicleTypeString = args[0].GetText().Trim('"');
            }

            System.Diagnostics.Debug.WriteLine($"Vehicle Type String: {vehicleTypeString}");

            if (!Enum.TryParse(vehicleTypeString, true, out VehicleType vehicleType))
            {
                throw new ArgumentException($"Invalid vehicle type: {vehicleTypeString}");
            }

            System.Diagnostics.Debug.WriteLine($"Parsed Vehicle Type: {vehicleType}");

            npc.VehicleType = vehicleType;

            npc.InitialPosition = ParseLanePosition(args[1], variableMap);
            npc.Goal = ParseLanePosition(args[2], variableMap);

            // Handle routes if present
            if (args.Length > 3) // still need to edit
            {
                string routesText;
                if (args[3].variableExp() != null)
                {
                    if (!variableMap.TryGetValue(args[3].GetText(), out routesText))
                    {
                        throw new ArgumentException($"Variable {args[3].GetText()} not found in variable map");
                    }
                }
                else
                {
                    routesText = args[3].GetText().Trim('"');
                }

                var routes = routesText.Trim('[', ']', '"').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var route in routes)
                {
                    var parts = route.Split(new[] { "with-speed-limit" }, StringSplitOptions.None);
                    var lane = parts[0].Trim().Trim('"'); // Added Trim('"') to handle extra quotes
                    float speed = parts.Length > 1 ? float.Parse(parts[1].Trim()) : 0;

                    System.Diagnostics.Debug.WriteLine($"Route: {lane}, Speed: {speed}");

                    npc.Config.RouteSpeeds[lane] = speed;
                }
            }

            return npc;
        }


        private IPosition ParseLanePosition(AWSIMScriptGrammarParser.ExpressionContext context, Dictionary<string, string> variableMap)
        {
            string lanePositionText = context.GetText();
            if (context.variableExp() != null)
            {
                if (!variableMap.TryGetValue(lanePositionText, out lanePositionText))
                {
                    throw new ArgumentException($"Variable {lanePositionText} not found in variable map");
                }
            }

            var parts = lanePositionText.Split(new[] { "at" }, StringSplitOptions.None);
            var lane = parts[0].Trim('"');
            var offset = parts.Length > 1 ? float.Parse(parts[1].Trim()) : 0;

            return new LanePosition(lane, offset);
        }


        public static void Main()
        {
            // This is an example showing how we can use Anltr to obtain the parsed Tree
            string input = File.ReadAllText("inputs/input.txt");
            ICharStream stream = CharStreams.fromString(input);
            ITokenSource lexer = new AWSIMScriptGrammarLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            AWSIMScriptGrammarParser parser = new AWSIMScriptGrammarParser(tokens);
            IParseTree tree = parser.scenario();
            Console.Write(tree.ToStringTree());
        }
    }
}

