using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerAgent : MonoBehaviour
{
    [SerializeField]
    private GameObject startNode, goalNode;
    private Vector3 startNodePos, goalNodePos;
    private float startToEndDistanceX, startToEndDistanceY, currentToStartDistance, currentToGoalDistance;
    // Start is called before the first frame update
    public void AgentStep()
    {
        Debug.Log("Agent moving");
        GameObject[] possiblePaths = GameObject.FindGameObjectsWithTag("PathPrefab");
        foreach (GameObject possiblePath in possiblePaths)
        {
            Debug.Log("Found object: " + possiblePath.name);
            SCR_PossiblePathNode pathNodeScriptRef = possiblePath.gameObject.GetComponent<SCR_PossiblePathNode>();
            if(pathNodeScriptRef != null)
            {
                possiblePath.GetComponent<SCR_PossiblePathNode>().SetPlayerReference(this.gameObject);
                possiblePath.GetComponent<SCR_PossiblePathNode>().CalculatePathScores();
            }
            else { Debug.Log("path node script not found, NEE NORRRR NEE NORRRRRRRRR!!!!!!!"); }
        }

        gameObject.transform.position = startNodePos;
    }

    public void StartToEndGoalDistance()
    {
        startNodePos = startNode.transform.position;
        goalNodePos = goalNode.transform.position;
        Debug.Log("Start node is located at: " + startNodePos);
        Debug.Log("Goal node is located at: " +  goalNodePos);
        startToEndDistanceX = goalNodePos.x - startNodePos.x;
        startToEndDistanceY = goalNodePos.y - startNodePos.y;
        var StartToEndDistance = new Vector2(startToEndDistanceX, startToEndDistanceY);
        Debug.Log("Distance from start to end is: " + StartToEndDistance);
    }
}
