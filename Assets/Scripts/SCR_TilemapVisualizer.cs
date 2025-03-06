using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/***************************************************************************************
*    Title: Adding Floor Tiles - P6 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 18/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=W6cBwk0bRWE
*    Availability git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/TilemapVisualizer.cs
***************************************************************************************/

public class SCR_TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    public Tilemap floorTilemap, wallTilemap;

    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, WallBottom, wallFull, wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft, wallDiagonalCornerDownRight;

    //Uses IEnumerable as a variable to store generic collections
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            paintSingleTile(tilemap, tile, position);
        }
    }

    public void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        //Looks through wall types data script hashsets that contain the binary for each wall piece, if it matches it paints a wall of that type
        if (SRC_WallTypesData.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (SRC_WallTypesData.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (SRC_WallTypesData.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (SRC_WallTypesData.wallBottom.Contains(typeAsInt))
        {
            tile = WallBottom;
        }
        else if (SRC_WallTypesData.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        if (tile != null)
        {
            paintSingleTile(wallTilemap, tile, position);
        }
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(SRC_WallTypesData.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (SRC_WallTypesData.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (SRC_WallTypesData.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (SRC_WallTypesData.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (SRC_WallTypesData.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (SRC_WallTypesData.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (SRC_WallTypesData.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (SRC_WallTypesData.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = WallBottom;
        }
        if (tile != null)
        {
            paintSingleTile(wallTilemap, tile, position);
        }
    }
}
