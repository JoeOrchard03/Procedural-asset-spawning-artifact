using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GridManager : MonoBehaviour
{
    public Dictionary<Vector2, SCR_NodeBase> Tiles { get; private set; }

    public SCR_NodeBase GetTileAtPosition(Vector2 pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;
}
