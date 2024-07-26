using System;
namespace AWSIM_Script.Object
{
    /// <summary>
    /// A pair of lane and offset position from the starting point of the lane
    /// This is used for many purpose, e.g., to specify location for spawning NPCs
    /// </summary>
    public class LanePosition : IPosition
    {
        public string LaneName { get; set; }
        // distance from the starting point of the lane
        public float Position { get; set; }
        public LanePosition(string laneName, float position)
        {
            LaneName = laneName;
            Position = position;
        }
    }
}

