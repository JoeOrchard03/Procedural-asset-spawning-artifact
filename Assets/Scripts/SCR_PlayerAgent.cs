using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCR_PlayerAgent : MonoBehaviour
{
    [SerializeField]
    private GameObject startNode, goalNode;
    private Vector3 startNodePos, goalNodePos;
    private float startToEndDistanceX, startToEndDistanceY, currentToStartDistance, currentToGoalDistance;
    // Start is called before the first frame update

    public static List<SCR_NodeBase> FindPath(SCR_NodeBase startNode, SCR_NodeBase goalNode)
    {
        var toSearch = new List<SCR_NodeBase>() { startNode };
        var processed = new List<SCR_NodeBase>();

        //While there are elements inside the toSearch list
        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach(var node in toSearch)
            {
                //If the node being checked has a lower F score, or the same F score but with a lower H score
                if (node.F < current.F || node.F == current.F && node.H < current.H)
                {
                    //Change current to be the node being checked
                    current = node;
                }
            }

            //Add the node to processed after it is processed
            processed.Add(current);
            //Remove it from the to be searched que
            toSearch.Remove(current);

            //Checks if the current node is the goal node
            if (current == goalNode)
            {
                var currentPathTile = goalNode;
                //create a new list that contains the best path from goal to start node
                var path = new List<SCR_NodeBase>();
                while (currentPathTile != startNode)
                {
                    //Adds the current path tile to the list
                    path.Add(currentPathTile);
                    //Sets the currentPathTile to the the connection of the currentPathTile
                    currentPathTile = currentPathTile.Connection;
                }

                //Returns the completed path
                return path;
            }

            //Checks non processed neighbours of the cheapest movement cost node
            foreach(var neighbour in current.Neighbours.Where(node => !processed.Contains(node)))
            {
                var inSearch = toSearch.Contains(neighbour);

                //calculates the cost to get to the neighbour from the current best node
                var costToNeighbour = current.G + current.GetDistance(neighbour);

                //If the new calculated cost is less then the current G cost of the neighbour update it
                if(!inSearch || costToNeighbour < neighbour.G)
                {
                    neighbour.SetG(costToNeighbour);
                    //Sets that neighbours connection to the current node to be able to retrack the path after the route is found
                    neighbour.SetConnection(current);

                    if(!inSearch)
                    {
                        //Update the H cost and add it to be searched if it has not been yet
                        neighbour.SetH(neighbour.GetDistance(goalNode));
                        toSearch.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    public void AgentStep()
    {
        //Debug.Log("Agent moving");
        //GameObject[] possiblePaths = GameObject.FindGameObjectsWithTag("PathPrefab");
        //foreach (GameObject possiblePath in possiblePaths)
        //{
        //    Debug.Log("Found object: " + possiblePath.name);
        //    SCR_PossiblePathNode pathNodeScriptRef = possiblePath.gameObject.GetComponent<SCR_PossiblePathNode>();
        //    if(pathNodeScriptRef != null)
        //    {
        //        possiblePath.GetComponent<SCR_PossiblePathNode>().SetPlayerReference();
        //        possiblePath.GetComponent<SCR_PossiblePathNode>().CalculatePathScores();
        //    }
        //    else { Debug.Log("path node script not found, NEE NORRRR NEE NORRRRRRRRR!!!!!!!"); }
        //}

        //gameObject.transform.position = startNodePos;
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
