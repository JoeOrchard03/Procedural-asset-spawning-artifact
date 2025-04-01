using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SCR_GridManager : MonoBehaviour
{
    public Dictionary<Vector2, SCR_NodeBase> Tiles { get; private set; }

    [SerializeField] public HashSet<Vector2Int> floorTiles;

    public GameObject[] floorTileObjs;


    private Vector2 tilePosition;

    public void GetNeighbours()
    {
        floorTileObjs = GameObject.FindGameObjectsWithTag("tile");

        foreach (GameObject tile in floorTileObjs)
        {
            tile.GetComponent<SCR_PossiblePathNode>().walkable = true;
            tile.GetComponent<SCR_PossiblePathNode>().CacheNeighbors();
        }
    }

    public SCR_NodeBase GetTileAtPosition(Vector2 pos, bool corridorTiles = false)
    {
        tilePosition = pos;

        Vector3 rayOrigin = new Vector3(pos.x, pos.y, -10.0f);

        if(corridorTiles)
        {
            Debug.DrawRay(rayOrigin, new Vector3(0, 0, 1) * 20.0f, Color.blue, Mathf.Infinity);
        }
        
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
