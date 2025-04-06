using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCR_PlayerAgent : MonoBehaviour
{
    [SerializeField]
    private GameObject startNode, goalNode;
    private Vector3 startNodePos, goalNodePos;
    private float startToEndDistanceX, startToEndDistanceY;

    [SerializeField] public List<SCR_NodeBase> pathNodes;

    public void FindPath()
    {
        pathNodes = SCR_Pathfinding.FindPath(startNode.GetComponent<SCR_NodeBase>(), goalNode.GetComponent<SCR_NodeBase>());
    }

    public void StartToEndGoalDistance()
    {
        startNodePos = startNode.transform.position;
        goalNodePos = goalNode.transform.position;
        //Debug.Log("Start node is located at: " + startNodePos);
        //Debug.Log("Goal node is located at: " +  goalNodePos);
        startToEndDistanceX = goalNodePos.x - startNodePos.x;
        startToEndDistanceY = goalNodePos.y - startNodePos.y;
        var StartToEndDistance = new Vector2(startToEndDistanceX, startToEndDistanceY);
        //Debug.Log("Distance from start to end is: " + StartToEndDistance);
    }


}
