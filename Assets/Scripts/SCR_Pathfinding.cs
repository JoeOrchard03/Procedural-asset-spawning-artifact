using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public static class SCR_Pathfinding
{
    // Start is called before the first frame update
    public static List<SCR_NodeBase> FindPath(SCR_NodeBase startNodePrefab, SCR_NodeBase goalNodePrefab)
    {
        SCR_NodeBase startNode = getTile(startNodePrefab);
        SCR_NodeBase goalNode = getTile(goalNodePrefab);

        var toSearch = new List<SCR_NodeBase>() { startNode };
        var processed = new List<SCR_NodeBase>();
        //While there are elements inside the toSearch list
        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach (var node in toSearch)
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
            GameObject currentObj = current.gameObject.GetComponent<SCR_PossiblePathNode>().getSelfGameObject();
            //currentObj.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            //Remove it from the to be searched que
            toSearch.Remove(current);

            //Checks if the current node is the goal node
            if (current.getSelfPos() == goalNode.getSelfPos())
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

                foreach(var tile in path)
                {
                    tile.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    tile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                }

                //Returns the completed path
                Debug.Log("Returning a completed path, goal found");
                return path;
            }

            if (current.Neighbours == null)
            {
                Debug.LogError($"Node {current} has a null Neighbours list!");
                return null;
            }

            //Checks non processed neighbours of the cheapest movement cost node
            foreach (var neighbour in current.Neighbours.Where(node => !processed.Contains(node)))
            {
                var inSearch = toSearch.Contains(neighbour);

                //calculates the cost to get to the neighbour from the current best node
                var costToNeighbour = current.G + current.GetDistance(neighbour);

                //If the new calculated cost is less then the current G cost of the neighbour update it
                if (!inSearch || costToNeighbour < neighbour.G)
                {
                    neighbour.SetG(costToNeighbour);
                    //Sets that neighbours connection to the current node to be able to retrack the path after the route is found
                    neighbour.SetConnection(current);

                    if (!inSearch)
                    {
                        //Update the H cost and add it to be searched if it has not been yet
                        neighbour.SetH(neighbour.GetDistance(goalNode));
                        toSearch.Add(neighbour);
                    }
                }
            }
        }
        Debug.Log("path being returned is null");
        return null;
    }

    private static SCR_NodeBase getTile(SCR_NodeBase NodePrefab)
    {
        Vector3 pos = NodePrefab.gameObject.transform.position;

        Vector3 rayOrigin = new Vector3(pos.x, pos.y, -10.0f);

        //Debug.DrawRay(rayOrigin, new Vector3(0, 0, 1) * 20.0f, Color.blue, Mathf.Infinity);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, new Vector3(0, 0, 1), out hit, Mathf.Infinity))
        {
            //Debug.Log($"Raycast hit: {hit.collider.gameObject.name} at {hit.collider.gameObject.transform.position}");
            return hit.collider.gameObject.GetComponent<SCR_NodeBase>();
        }
        //Debug.Log("node tile not found");
        return null;
    }
}
