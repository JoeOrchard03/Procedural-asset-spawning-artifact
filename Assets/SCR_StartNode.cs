using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCR_StartNode : SCR_NodeBase
{
    //[SerializeField] TMP_Text GScoreText, HScoreText, FScoreText;
    [SerializeField] private SCR_GridManager gridManagerInstance;

    private void Start()
    {
        gridManagerInstance = GameObject.Find("GridManager").GetComponent<SCR_GridManager>();
    }

    public override void CacheNeighbors()
    {
        Neighbours = new List<SCR_NodeBase>();

        foreach (var tile in Direction2D.cardinalDirectionsList.Select(dir => gridManagerInstance.GetTileAtPosition(new Vector2(transform.position.x, transform.position.y) + dir)).Where(tile => tile != null))
        {
            Debug.Log($"Tile that has been found is: {tile.name}");
            Neighbours.Add(tile);
        }
    }
}
