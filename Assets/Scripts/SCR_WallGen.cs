using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Placing Basic Walls - P9 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=LdpItlRg8OM&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=10
*    Availability git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/WallGenerator.cs
***************************************************************************************/

public static class SCR_WallGen
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, SCR_TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
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
