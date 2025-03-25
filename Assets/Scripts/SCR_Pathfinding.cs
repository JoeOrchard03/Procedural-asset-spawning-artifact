using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SCR_Pathfinding
{
    // Start is called before the first frame update
    public static List<SCR_NodeBase> FindPath(SCR_NodeBase startNode, SCR_NodeBase goalNode)
    {
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
            GameObject currentObj = current.getSelfGameObject();
            currentObj.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            //Remove it from the to be searched que
            toSearch.Remove(current);

            Debug.Log("current self pos is: " + current.getSelfPos());
            Debug.Log("goal node self pos is: " + goalNode.getSelfPos());

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

                //Returns the completed path
                return path;
            }

            if (current.Neighbours == null)
            {
                Debug.LogError($"Node {current} has a null Neighbours list!");
                return null;
            }

            Debug.Log($"current.Neighbours Count: {current.Neighbours.Count}");

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
        return null;
    }
}
