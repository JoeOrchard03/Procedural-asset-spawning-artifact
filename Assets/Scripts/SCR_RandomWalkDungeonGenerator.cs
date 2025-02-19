using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/***************************************************************************************
*    Title: Using Random Walk Algorithm - P5 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 18/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=LnbZLnCXSyI
*    Availability git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/SimpleRandomWalkDungeonGenerator.cs
***************************************************************************************/

public class SCR_RandomWalkDungeonGenerator : SCR_AbstractDungeonGen
{
    [SerializeField]
    protected SCR_RandomWalkSO randomWalkParameters;

    /// <summary>
    /// Runs the random walk algorithm for the amount of times set in Iterations and then prints out all the positions that the combined random walk algorithms generate
    /// </summary>
    protected override void RunProcGen()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPos);
        //Clear tilemap
        tilemapVisualizer.Clear();
        //paints all the tiles in floor positions to visualise them
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        SCR_WallGen.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SCR_RandomWalkSO parameters, Vector2Int position)
    {
        var currentPos = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var returnData = SCR_PCGAlgorithms.RandomWalk(currentPos, parameters.walkLength);
            //Union with adds the return data to floor positions but does not add any duplicates that are already in floorPositions
            floorPositions.UnionWith(returnData);
            if(parameters.startRandomlyEachIteration)
            {
                //Gets a random position inside floor positions to start the next random walk iteration from
                currentPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
