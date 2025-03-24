using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GridManager : MonoBehaviour
{
    public Dictionary<Vector2, SCR_NodeBase> Tiles { get; private set; }

    [SerializeField] public HashSet<Vector2Int> floorTiles;

    private void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

        foreach(GameObject tile in tiles)
        {
            Debug.Log($"tile: {tile.transform.name} reporting for duty");
            tile.GetComponent<SCR_PossiblePathNode>().CacheNeighbors();
        }
    }

    //public SCR_NodeBase GetTileAtPosition(Vector2 pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;

    public SCR_NodeBase GetTileAtPosition(Vector2 pos)
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.SphereCast(pos, 1.0f, transform.forward, out hit))
        {
            if (hit.collider.tag == "tile")
                print(this.name + "Hit!");
        }
        return null;
    }
}
