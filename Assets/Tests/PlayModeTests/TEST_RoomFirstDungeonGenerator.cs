using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class TEST_RoomFirstDungeonGenerator
{
    private SCR_RoomFirstDungeonGenerator scriptTestInstance;
    private SCR_RandomWalkSO randomWalkParams;
    private SCR_TilemapVisualizer tilemapVisualiserInstance;

    [SetUp]
    public void SetUp()
    {
        GameObject testOBJ = new GameObject();
        scriptTestInstance = testOBJ.AddComponent<SCR_RoomFirstDungeonGenerator>();
        randomWalkParams = Resources.Load<SCR_RandomWalkSO>("RandomWalkParams_Defaults");
        scriptTestInstance.randomWalkParameters = randomWalkParams;
        tilemapVisualiserInstance = new GameObject().AddComponent<SCR_TilemapVisualizer>(); 
    }

    [UnityTest]
    public IEnumerator RoomFirstDungeonGeneratorWithRandomWalkPasses_Test()
    {
        scriptTestInstance.randomWalkRooms = true;
        scriptTestInstance.CreateRooms(false);
        Assert.IsNotNull(tilemapVisualiserInstance, "TilemapVisualizer is null!");
        yield return null;
    }

    [UnityTest]
    public IEnumerator RoomFirstDungeonGeneratorWithoutRandomWalkPasses_Test()
    {
        scriptTestInstance.randomWalkRooms = false;
        scriptTestInstance.CreateRooms(false);
        Assert.IsNotNull(tilemapVisualiserInstance, "TilemapVisualizer is null!");
        yield return null;
    }
}
