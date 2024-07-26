using System;
using AWSIM_Script.Object;

namespace AWSIM_Script.Object
{
	public class NPCScriptObject
	{
		public NPCScriptObject()
		{
		}
        private IPosition initialPosition;
        private IPosition goal;
		private NPCSpawnDelay spawnDelayOption;
		private NPCConfig config;
    }
}

