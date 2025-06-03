using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/***************************************************************************************
*Title: Pathfinding - Understanding A * (A star)
*Author: Tarodev https://www.youtube.com/@Tarodev
*Date: 16 / 11 / 2021
*Availability youtube tutorial link: https://www.youtube.com/watch?v=i0x5fj4PqP4&t=255s
*Availability git link: https://github.com/Matthew-J-Spencer/Pathfinding/blob/main/_Scripts/Tiles/SquareNode.cs
***************************************************************************************/

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

        foreach (Vector2 dir in Direction2D.cardinalDirectionsList)
        {
            Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
            //Debug.Log($"{gameObject.transform.name} current position is: {currentPos}");
            //Debug.Log("raycast position is: " + new Vector2(transform.position.x + dir.x, transform.position.y + dir.y));
            var tile = gridManagerInstance.GetTileAtPos(new Vector2(transform.position.x + dir.x, transform.position.y + dir.y));
            if (tile != null)
            {
                //Debug.Log($"tile that has been found is: {tile.gameObject.name}");
                Neighbours.Add(tile);
            }
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

    public override GameObject getSelfGameObject()
    {
        return gameObject;
    }
}
