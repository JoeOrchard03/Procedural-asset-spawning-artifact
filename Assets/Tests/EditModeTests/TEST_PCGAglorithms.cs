using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TEST_PCGAglorithms
{
    [Header("Random walk test variables")]
    Vector2Int startPos;
    int walkLength;
    HashSet<Vector2Int> randomWalkResult;

    [Header("Binary Space Partitioning test variables")]
    int minRoomHeight;
    int minRoomWidth;
    int dungeonHeight;
    int dungeonWidth;
    int underMinHeight = 9;
    int underMinWidth = 9; 

    BoundsInt binarySpaceBoundsToSplit;
    List<BoundsInt> roomList;

    [SetUp]
    public void SetUp()
    {
        startPos = new(0, 0);
        walkLength = 10;
        randomWalkResult = SCR_PCGAlgorithms.RandomWalk(startPos, walkLength);

        minRoomHeight = 10;
        minRoomWidth = 10;
        dungeonHeight = 100;
        dungeonWidth = 100;

        underMinHeight = 9;
        underMinWidth = 9;

        binarySpaceBoundsToSplit = new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0));
        roomList = SCR_PCGAlgorithms.BinarySpacePartitioning(binarySpaceBoundsToSplit, minRoomWidth, minRoomHeight);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void RandomWalkCorrectLength_TEST()
    {
        Assert.IsTrue(walkLength + 1 >= randomWalkResult.Count, "Random walk too big");
    }

    [Test]
    public void RandomWalkContainsStartPos_TEST()
    {
        Assert.IsTrue(randomWalkResult.Contains(startPos), "Start not inside random walk");
    }

    [Test]
    public void BinarySpaceRoomsMeetMinimumRequirements_TEST()
    {
        foreach (var room in roomList)
        {
            Assert.IsTrue(room.size.y >= minRoomHeight, "Room is too short");
            Assert.IsTrue(room.size.x >= minRoomWidth, "Room is too thin");
        }
    }

    [Test]
    public void BinarySpaceRoomsNotBeSplitBeyondMinimum_TEST()
    {
        BoundsInt underMinimumBounds = new BoundsInt((Vector3Int)startPos, new Vector3Int(underMinWidth, underMinHeight, 0));
        roomList = SCR_PCGAlgorithms.BinarySpacePartitioning(underMinimumBounds, minRoomWidth, minRoomHeight);

        Assert.AreEqual(0, roomList.Count, "This space is too small to be split into more rooms");
    }
}
