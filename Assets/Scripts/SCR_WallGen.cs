using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SCR_WallGen
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, SCR_TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.directionsList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }

    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        //Iterates through every piece of floor in floor positions
        foreach (var position in floorPositions)
        {
            //Checks each direction
            foreach (var direction in directionsList)
            {
                //Adds direction to each floor piece
                var neighbourPosition = position + direction;
                //If the result is not in floor positions it means it is outside the map so a wall should be placed in that direction
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
