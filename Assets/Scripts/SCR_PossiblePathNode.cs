using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SCR_PossiblePathNode : SCR_NodeBase
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

    public struct SquareCoords : ICoords
    {
        public float GetDistance(ICoords other)
        {
            var dist = new Vector2Int(Mathf.Abs((int)Pos.x - (int)other.Pos.x), Mathf.Abs((int)Pos.y - (int)other.Pos.y));

            var lowest = Mathf.Min(dist.x, dist.y);
            var highest = Mathf.Max(dist.x, dist.y);

            var horizontalMovesRequired = highest - lowest;

            return lowest * 14 + horizontalMovesRequired * 10;
        }

        public Vector2 Pos { get; set; }
    }

    //private void UpdateScoreText()
    //{
    //    GScoreText.text = "G: " + G;
    //    HScoreText.text = "H: " + H;
    //    FScoreText.text = "F: " + F;
    //}
}
