using System.Collections.Generic;
using AWSIM_Script.Object;
using AWSIM_Script.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        string input1 = File.ReadAllText("inputs/input.txt");
        Scenario scenario = new ScriptParser().ParseScript(input1);
        Assert.AreEqual(scenario.NPCs.Count, 3);

        NPCScriptObject npc1 = scenario.NPCs[0];
        Assert.AreEqual(npc1.VehicleType, VehicleType.TAXI);
        AssertPositionEqual(npc1.InitialPosition, "TrafficLane.239", 15);
        AssertPositionEqual(npc1.Goal, "TrafficLane.265", 60);

        System.Diagnostics.Debug.WriteLine($"npc1 RouteSpeeds Count: {npc1.Config.RouteSpeeds.Count}");
        foreach (var route in npc1.Config.RouteSpeeds)
        {
            System.Diagnostics.Debug.WriteLine($"Route: Lane={route.Key}, Speed={route.Value}");
        }

        Assert.AreEqual(npc1.Config.RouteSpeeds.Count, 3);
        Assert.AreEqual(npc1.Config.RouteSpeeds.TryGetValue("TrafficLane.239", out float speed), true);
        Assert.AreEqual(speed, 0);
        Assert.AreEqual(npc1.Config.RouteSpeeds.TryGetValue("TrafficLane.448", out speed), true);
        Assert.AreEqual(speed, 20);
        Assert.AreEqual(npc1.Config.RouteSpeeds.TryGetValue("TrafficLane.265", out speed), true);
        Assert.AreEqual(speed, 7);

        NPCScriptObject npc2 = scenario.NPCs[1];
        Assert.AreEqual(npc2.VehicleType, VehicleType.VAN);
        AssertPositionEqual(npc2.InitialPosition, "TrafficLane.240", 0);
        AssertPositionEqual(npc2.Goal, "TrafficLane.241", 0);

        Assert.AreEqual(npc2.Config.RouteSpeeds.Count, 0);
    }

    public void AssertPositionEqual(IPosition position, string lane, float offset)
    {
        Assert.AreEqual(((LanePosition)position).LaneName, lane);
        Assert.AreEqual(((LanePosition)position).Position, offset);
    }
}
