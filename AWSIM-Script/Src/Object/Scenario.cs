using System;
namespace AWSIM_Script.Object
{
	// please add more necessary attributes
	public class Scenario
	{
        // list of NPCs
        public List<NPCScriptObject> NPCs { get; set; }

        public Scenario()
		{
            NPCs = new List<NPCScriptObject>();
        }
        
		// some more config might be added later
    }
}

