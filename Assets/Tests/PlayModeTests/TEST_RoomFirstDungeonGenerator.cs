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

    [SetUp]
    public void SetUp()
    {
        GameObject testOBJ = new GameObject();
        testOBJ.AddComponent<SCR_RoomFirstDungeonGenerator>();
        scriptTestInstance = testOBJ.GetComponent<SCR_RoomFirstDungeonGenerator>();
        randomWalkParams = Resources.Load<SCR_RandomWalkSO>("RandomWalkParams_Defaults");
        scriptTestInstance.randomWalkParameters = randomWalkParams;
    }

    [UnityTest]
    public IEnumerator TEST_RoomFirstDungeonGeneratorWithEnumeratorPasses()
    {
        //scriptTestInstance.randomWalkRooms = true;
        //scriptTestInstance.CreateRooms();
        //Assert.IsNotNull(scriptTestInstance.tilemapVisualizer, "TilemapVisualizer is null!");
        yield return null;
    }
}
