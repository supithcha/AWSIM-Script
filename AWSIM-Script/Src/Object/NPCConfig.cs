using System;
namespace AWSIM_Script.Object
{
	public class NPCConfig
	{
		public NPCConfig()
		{
		}

		// routes and (optional) desired speed limit
		// a map from lane name to the desired speed limit
		// if the speed limit is not set by the user, it is 0
		public Dictionary<string, float> RouteSpeeds { get; set; }

        // in future, may consider adding acceleration and deacceleration rates
    }
}


