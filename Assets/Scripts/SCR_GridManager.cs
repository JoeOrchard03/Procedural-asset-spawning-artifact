using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SCR_GridManager : MonoBehaviour
{
    public Dictionary<Vector2, SCR_NodeBase> Tiles { get; private set; }

    [SerializeField] public HashSet<Vector2Int> floorTiles;

    public int tileCounter;

    private Vector2 tilePosition;

    public void GetNeighbours()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<SCR_PossiblePathNode>().CacheNeighbors();
            tileCounter++;
            tile.name += " " + tileCounter.ToString();
        }
    }

    public SCR_NodeBase GetTileAtPosition(Vector2 pos)
    {
        tilePosition = pos;

        Vector3 rayOrigin = new Vector3(pos.x, pos.y, -10.0f);

        //Debug.DrawRay(rayOrigin, new Vector3(0,0,1) * 20.0f, Color.blue, Mathf.Infinity);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, new Vector3(0, 0, 1), out hit, Mathf.Infinity))
        {
            //Debug.Log($"Raycast hit: {hit.collider.gameObject.name} at {hit.collider.gameObject.transform.position}");
            return hit.collider.gameObject.GetComponent<SCR_NodeBase>();
        }

        //Debug.Log($"No colliders found at position {pos}");
        return null;
    }
}
