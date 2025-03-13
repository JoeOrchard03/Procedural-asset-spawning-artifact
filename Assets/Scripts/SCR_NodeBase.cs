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
    //Node that the current node originated from
    public SCR_NodeBase Connection { get; private set; }
    // G score movement cost from current node to start node
    public float G { get; private set; }
    //H score estimated cost from current node to goal node
    public float H { get; private set; }
    //F score total of G score and H score, first score checked to determine next moe
    public float F => G + H;

    //Setting the connction of the current node
    public void SetConnection(SCR_NodeBase nodeBase) => Connection = nodeBase;

    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;

    //public void GetDistance()
}