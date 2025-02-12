using System;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Corridors First Algorithm - P10 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability: https://www.youtube.com/watch?v=fsMDWutpo8g&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=10
***************************************************************************************/

public class SCR_CorridorFirstGen : SCR_RandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    [Tooltip("How likely a room is to branch off from a piece of corridor")]
    private float roomPercent = 0.8f;
    [SerializeField]
    public SCR_RandomWalkSO roomGenerationParams;

    protected override void RunProcGen()
    {
        CorridorFirstGen();
    }

    private void CorridorFirstGen()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        CreateCorridors(floorPositions);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        SCR_WallGen.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions)
    {
        var currentPosition = startPos;
        //Iterate through however many corridors there are to be created
        for(int i = 0; i < corridorCount; i++)
        {
            //Create corridor using function from PCGAlgorithms script
            var corridor = SCR_PCGAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            //Gets the last piece of corridor that was painted on the tilemap and sets it to be where the next one begins, ensuring they are all linked
            currentPosition = corridor[corridor.Count -1];
            //Unions with the floor positions that already exist to ensure that any duplicates are removed and no tile is painted twice
            floorPositions.UnionWith(corridor);
        }
    }
}
