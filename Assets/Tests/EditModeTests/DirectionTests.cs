using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DirectionTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void CardinalDirectionsCorrectValues_Test()
    {
        Assert.AreEqual(new Vector2Int(0, 1), Direction2D.cardinalDirectionsList[0], "Up direction is incorrect");
        Assert.AreEqual(new Vector2Int(1, 0), Direction2D.cardinalDirectionsList[1], "Right direction is incorrect");
        Assert.AreEqual(new Vector2Int(0, -1), Direction2D.cardinalDirectionsList[2], "Down direction is incorrect");
        Assert.AreEqual(new Vector2Int(-1, 0), Direction2D.cardinalDirectionsList[3], "Left direction is incorrect");
    }
}
