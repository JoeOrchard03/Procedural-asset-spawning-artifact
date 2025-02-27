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
    public void Setup()
    {
        scriptTestInstance = new GameObject().AddComponent<SCR_CorridorFirstDungeonGen>();
    }

    // PlayMode test for Corridor Length
    [UnityTest]
    public IEnumerator CorridorLengthCalculatedCorrectly_Test()
    {
        // Setup
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        // Act: Create corridors
        List<List<Vector2Int>> corridors = scriptTestInstance.CreateCorridors(floorPositions, potentialRoomPositions);

        // Yield until the next frame to ensure any async operations finish
        yield return null;

        // Assert: Check if corridor length is calculated correctly
        foreach (var corridor in corridors)
        {
            // Add one to account for random walk script adding the starting position of the corridor to the count
            Assert.AreEqual(scriptTestInstance.corridorLength + 1, corridor.Count, "Corridor length does not match expected value");
        }
    }

    // PlayMode test for DeadEnd detection
    [UnityTest]
    public IEnumerator DeadEndDetection_Test()
    {
        // Setup
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 0),
            new Vector2Int(2, 0),
        };

        // Act: Find all dead ends
        List<Vector2Int> deadEnds = scriptTestInstance.findAllDeadEnds(floorPositions);

        // Yield until the next frame if necessary (if the method is async)
        yield return null;

        // Assert: Check if the dead end was correctly detected
        Assert.IsTrue(deadEnds.Contains(new Vector2Int(2, 0)), "Dead end detection failed");
    }

    // PlayMode test for Room Generation Percentage
    [UnityTest]
    public IEnumerator RoomGenerationPercentage_Test()
    {
        // Setup potential room positions
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 1),
            new Vector2Int(2, 2),
            new Vector2Int(3, 3),
            new Vector2Int(4, 4),
        };

        // Act: Call CreateRooms and check the generated room positions
        HashSet<Vector2Int> roomPositions = scriptTestInstance.CreateRooms(potentialRoomPositions);

        // Assert that the roomPositions are not null
        Assert.IsNotNull(roomPositions, "CreateRooms returned null");

        // Yield until the next frame if necessary
        yield return null;

        // Assert the expected room count based on the percentage
        int expectedRoomCount = Mathf.RoundToInt(potentialRoomPositions.Count * scriptTestInstance.roomPercent);
        Assert.AreEqual(expectedRoomCount, roomPositions.Count, "Room count does not match expected percentage");
    }
}
