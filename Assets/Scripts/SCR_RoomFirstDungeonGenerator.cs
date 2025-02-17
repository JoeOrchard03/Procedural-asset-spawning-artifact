using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/***************************************************************************************
*    Title: Room First Generation - P15 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability: https://www.youtube.com/watch?v=pWZg1oChtnc&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=16
***************************************************************************************/

public class SCR_RoomFirstDungeonGenerator : SCR_RandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;

    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;

    [SerializeField]
    [Range(0,10)]
    [Tooltip("Makes sure there is blank space seperating the rooms")]
    private int offset = 1;

    [SerializeField]
    [Tooltip("Whether you want to use random walk to help generate the rooms")]
    private bool randomWalkRooms = false;

    protected override void RunProcGen()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        //Run the binary space partitioning algorithm to generate the list of rooms
        var roomsList = SCR_PCGAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, 
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);

        tilemapVisualizer.PaintFloorTiles(floor);
        SCR_WallGen.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = 0; col < room.size.x - offset; col++)
            {
                for (int row = 0; row < room.size.y - offset; row++)
                {
                    //foreach position that is inside the bounds of the room add it to the floor hashset
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
