using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TEST_CorridorFirstDungeonGen
{
    private SCR_CorridorFirstDungeonGen scriptTestInstance;

    [SetUp]
    public void SetUp()
    {
        scriptTestInstance = new GameObject().AddComponent<SCR_CorridorFirstDungeonGen>();
    }

    [UnityTest]
    public IEnumerator CorridorLengthCalculatedCorrectly_Test()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        List<List<Vector2Int>> corridors = scriptTestInstance.CreateCorridors(floorPositions, potentialRoomPositions);

        foreach (var corridor in corridors)
        {
            // Add one to account for random walk script adding the starting position of the corridor to the count
            Assert.AreEqual(scriptTestInstance.corridorLength + 1, corridor.Count, "Corridor length does not match expected value");
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator DeadEndDetection_Test()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 0),
            new Vector2Int(2, 0),
        };

        List<Vector2Int> deadEnds = scriptTestInstance.FindAllDeadEnds(floorPositions);

        //(2,0) is a dead end because it only has a floor position in one of the cardinal directions
        Assert.IsTrue(deadEnds.Contains(new Vector2Int(2, 0)), "Dead end detection failed");

        yield return null;
    }

    [UnityTest]
    public IEnumerator CorridorContainsLastCorridorEndTile_Test()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        List<List<Vector2Int>> corridors = scriptTestInstance.CreateCorridors(floorPositions, potentialRoomPositions);

        
        yield return null;
    }
}
