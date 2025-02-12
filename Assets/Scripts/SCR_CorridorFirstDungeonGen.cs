using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/***************************************************************************************
*    Title: Corridors First Algorithm - P10 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability: https://www.youtube.com/watch?v=fsMDWutpo8g&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=10
***************************************************************************************/

public class SCR_CorridorFirstDungeonGen : SCR_RandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    [Tooltip("How likely a room is to branch off from a piece of corridor")]
    private float roomPercent = 0.8f;

    protected override void RunProcGen()
    {
        CorridorFirstGen();
    }

    private void CorridorFirstGen()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = findAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);
        
        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        SCR_WallGen.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach(var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> findAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach(var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.directionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }
            if(neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        //Gets the count of rooms that are to be generated
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        //Randomly sorts the potential room positions hashset and extracts the amount of rooms to create count, returned as a list
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x =>Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach(var roomPosition in roomsToCreate)
        {
            //Creates room originating from room position
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPos;
        potentialRoomPositions.Add(currentPosition);

        //Iterate through however many corridors there are to be created
        for(int i = 0; i < corridorCount; i++)
        {
            //Create corridor using function from PCGAlgorithms script
            var corridor = SCR_PCGAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            //Gets the last piece of corridor that was painted on the tilemap and sets it to be where the next one begins, ensuring they are all linked
            currentPosition = corridor[corridor.Count -1];
            //Adds the end of the corridor to a hashset of potential positions for new rooms
            potentialRoomPositions.Add(currentPosition);
            //Unions with the floor positions that already exist to ensure that any duplicates are removed and no tile is painted twice
            floorPositions.UnionWith(corridor);
        }
    }
}
