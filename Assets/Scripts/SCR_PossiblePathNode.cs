using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using _Scripts.Tiles;
using System.Linq;
using Tarodev_Pathfinding._Scripts.Grid;

public class SCR_PossiblePathNode : SCR_NodeBase
{
    [SerializeField] TMP_Text GScoreText, HScoreText, FScoreText;
    [SerializeField] private SCR_GridManager gridManagerInstance;

    private void Start()
    {
        gridManagerInstance = GameObject.Find("GridManager").GetComponent<SCR_GridManager>();
    }

    public override void CacheNeighbors()
    {
        Neighbours = new List<SCR_NodeBase>();

        foreach (var tile in Direction2D.cardinalDirectionsList.Select(dir => GridManager.Instance.GetTileAtPosition(new Vector2(transform.position.x, transform.position.y) + dir)).Where(tile => tile != null))
        {
            Neighbours.Add(tile);
        }
    }

    private void UpdateScoreText()
    {
        GScoreText.text = "G: " + G;
        HScoreText.text = "H: " + H;
        FScoreText.text = "F: " + F;
    }
}
