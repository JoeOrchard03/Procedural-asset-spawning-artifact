using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TEST_RandomWalkDungeonGenerator
{

    private SCR_RandomWalkDungeonGenerator scriptTestInstance;
    private SCR_RandomWalkSO randomWalkParamsToTest;

    [SetUp]
    public void SetUp()
    {
        scriptTestInstance = new GameObject().AddComponent<SCR_RandomWalkDungeonGenerator>();
        randomWalkParamsToTest = Resources.Load<SCR_RandomWalkSO>("RandomWalkParams_Defaults");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator RunRandomWalkReturnsValid_Test()
    {
        var TestResult = scriptTestInstance.RunRandomWalk(randomWalkParamsToTest, new Vector2Int(0,0));
        Assert.NotNull(TestResult, "Run random walk returning null");
        yield return null;
    }
}
