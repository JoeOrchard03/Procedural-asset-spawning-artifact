using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TEST_Directions
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

    [Test]
    public void DiagonalDirectionsCorrectValues_Test()
    {
        Assert.AreEqual(new Vector2Int(1, 1), Direction2D.diagonalDirectionsList[0], "Up right direction is incorrect");
        Assert.AreEqual(new Vector2Int(1, -1), Direction2D.diagonalDirectionsList[1], "Right down direction is incorrect");
        Assert.AreEqual(new Vector2Int(-1, -1), Direction2D.diagonalDirectionsList[2], "Down left direction is incorrect");
        Assert.AreEqual(new Vector2Int(-1, 1), Direction2D.diagonalDirectionsList[3], "Left up direction is incorrect");
    }

    [Test]
    public void EightDirectionsCorrectValues_Test()
    {
        Assert.AreEqual(new Vector2Int(0, 1), Direction2D.eightDirectionsList[0], "Up direction is incorrect");
        Assert.AreEqual(new Vector2Int(1, 1), Direction2D.eightDirectionsList[1], "Up right direction is incorrect");
        Assert.AreEqual(new Vector2Int(1, 0), Direction2D.eightDirectionsList[2], "Right direction is incorrect");
        Assert.AreEqual(new Vector2Int(1, -1), Direction2D.eightDirectionsList[3], "Right down direction is incorrect");
        Assert.AreEqual(new Vector2Int(0, -1), Direction2D.eightDirectionsList[4], "Down direction is incorrect");
        Assert.AreEqual(new Vector2Int(-1, -1), Direction2D.eightDirectionsList[5], "Down left direction is incorrect");
        Assert.AreEqual(new Vector2Int(-1, 0), Direction2D.eightDirectionsList[6], "Left direction is incorrect");
        Assert.AreEqual(new Vector2Int(-1, 1), Direction2D.eightDirectionsList[7], "Left up direction is incorrect");
    }
}
