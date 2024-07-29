using System;
using AWSIM_Script.Object;

namespace AWSIM_Script.Object
{
	public enum VehicleType
	{
		TAXI,
        HATCHBACK,
		VAN,
		TRUCK,
		SMALL_CAR
	}

    public class NPCScriptObject
    {
        public NPCScriptObject()
        {
            Config = new NPCConfig();
        }

        public IPosition InitialPosition { get; set; }
        public IPosition Goal { get; set; }
        public NPCSpawnDelay SpawnDelayOption { get; set; }
        public NPCConfig Config { get; set; }
        public VehicleType VehicleType { get; set; }

        
    }

    //public class NPCScriptObject
    //{
    //	public NPCScriptObject()
    //	{
    //	}
    //       public IPosition InitialPosition { get; set; }
    //       public IPosition Goal { get; set; }
    //	public NPCSpawnDelay SpawnDelayOption { get; set; }
    //	public NPCConfig Config { get; set; } 
    //       public VehicleType VehicleType { get; set; }

    //   }
}

