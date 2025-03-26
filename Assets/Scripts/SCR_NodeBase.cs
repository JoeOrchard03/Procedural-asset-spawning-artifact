using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Pathfinding - Understanding A* (A star)
*    Author: Tarodev https://www.youtube.com/@Tarodev
*    Date: 16/11/2021
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=i0x5fj4PqP4&t=255s
*    Availability git link: https://github.com/Matthew-J-Spencer/Pathfinding/blob/main/_Scripts/Tiles/NodeBase.cs
***************************************************************************************/

public abstract class SCR_NodeBase : MonoBehaviour
{
    public ICoords Coords;
    public List<SCR_NodeBase> Neighbours { get; protected set; }
    //Node that the current node originated from
    public SCR_NodeBase Connection { get; private set; }
    // G score movement cost from current node to start node
    public float G { get; private set; }
    //H score estimated cost from current node to goal node
    public float H { get; private set; }
    //F score total of G score and H score, first score checked to determine next moe
    public float F => G + H;

    //Setting the connction of the current node
    public void SetConnection(SCR_NodeBase nodeBase)
    {
        Connection = nodeBase;
    }

    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;

    public abstract void CacheNeighbors();

    public float GetDistance(SCR_NodeBase nodeToGetDistanceTo)
    {
        float varToReturn = Mathf.Abs(gameObject.transform.position.x - nodeToGetDistanceTo.gameObject.transform.position.x) + Mathf.Abs(gameObject.transform.position.y - nodeToGetDistanceTo.gameObject.transform.position.y);
        return varToReturn; 
    }

    public abstract GameObject getSelfGameObject();

    public Vector2 getSelfPos()
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }
}

public interface ICoords
{
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}