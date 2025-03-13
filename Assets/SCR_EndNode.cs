using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCR_EndNode : SCR_NodeBase
{
    [SerializeField] private SCR_GridManager gridManagerInstance;

    private void Start()
    {
        gridManagerInstance = GameObject.Find("GridManager").GetComponent<SCR_GridManager>();
    }

    public override void CacheNeighbors()
    {
        Neighbours = new List<SCR_NodeBase>();

        foreach (var tile in Direction2D.cardinalDirectionsList.Select(dir => gridManagerInstance.GetTileAtPosition(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + dir)).Where(tile => tile != null))
        {
            Debug.Log(Neighbours);
            Neighbours.Add(tile);
        }
    }
}
