using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(SCR_TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach(var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    //if there is a floor in the position being checked add a 1 to mark it
                    neighboursBinaryType += "1";
                }
                else
                {
                    //if there is not a floor in the position being checked add a 0 to mark it
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWalls(SCR_TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryType = "";
            foreach(var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    //if there is a floor in the position being checked add a 1 to mark it
                    neighboursBinaryType += "1";
                }
                else
                {
                    //if there is not a floor in the position being checked add a 0 to mark it
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
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

    public static HashSet<Vector2Int> FindAgentPathInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList, Vector2Int playerPosition)
    {
        HashSet<Vector2Int> floorPositons = new HashSet<Vector2Int>();
        //Iterates through every piece of floor in floor positions
        //Checks each direction
        foreach (var direction in directionsList)
        {
            //Adds direction to each floor piece
            var neighbourPosition = playerPosition + direction;
            //If the result is not in floor positions it means it is outside the map so a wall should be placed in that direction
            if (floorPositions.Contains(neighbourPosition) == true)
            {
                floorPositions.Add(neighbourPosition);
            }
        }
        return floorPositions;
    }
}
