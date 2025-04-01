using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.ShaderData;
using Random = UnityEngine.Random;

/***************************************************************************************
*    Title: Room First Generation - P15 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=pWZg1oChtnc&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=16
*    Availability git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/RoomFirstDungeonGenerator.cs
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
    public bool randomWalkRooms = false;

    [SerializeField]
    private GameObject startDoor, endDoor, playerAgent;

    [SerializeField]
    private GameObject possiblePathPrefab;

    [SerializeField] private SCR_GridManager gridManagerInstance;

    public HashSet<Vector2Int> corridors;

    protected override void RunProcGen(bool generatePath)
    {
        CreateRooms(generatePath);
    }

    public void CreateRooms(bool generatePath)
    {
        //Run the binary space partitioning algorithm to generate the list of rooms
        var roomsList = SCR_PCGAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos,
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            //Gets the center of each room
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        FindBottomLeftRoom(roomsList);
        FindTopRightRoom(roomsList);
        corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);
        tilemapVisualizer.PaintFloorTiles(floor);
        SCR_WallGen.CreateWalls(floor, tilemapVisualizer);
        playerAgent.GetComponent<SCR_PlayerAgent>().StartToEndGoalDistance();

        StartCoroutine(GetPossiblePaths(floor, generatePath));
    }

    private IEnumerator GetPossiblePaths(HashSet<Vector2Int> floor, bool generatePath)
    {
        yield return new WaitForSeconds(0.25f);
        gridManagerInstance = GameObject.Find("GridManager").GetComponent<SCR_GridManager>();
        gridManagerInstance.floorTiles = floor;
        gridManagerInstance.GetNeighbours();
        //Debug.Log("gridManagerInstance populated with floor pieces");

        yield return new WaitForSeconds(0.25f);
        //Debug.Log("Getting possible paths");
        if(generatePath)
        {
            playerAgent.GetComponent<SCR_PlayerAgent>().FindPath();
        }
    }


    protected override void PathFindingAgentStep()
    {
        Debug.Log("AGENT STEPPING");
    }

    private Vector2Int Vector3ToVector2Int(Vector3 vectorToConvert)
    {
        int roundedX = (int)Math.Round(vectorToConvert.x);
        int roundedY = (int)Math.Round(vectorToConvert.y);
        var converetedVector = new Vector2Int(roundedX, roundedY);
        return converetedVector;
    }

    private Vector3 Vector2IntToVector3(Vector2Int vectorToConvert)
    {
        var convertedVector = new Vector3(vectorToConvert.x, vectorToConvert.y, 0);
        return convertedVector;
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];  
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            //Check if each piece of floor is inside the bounds of the room
            foreach(var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset)
                    && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    //Steps through positions by checking which direction to go and then moving until they have the same x and y values
    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while(position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;    
            }
            corridor.Add(position);
        }
        while(position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    //Find the closest room center to the current room center to decide where to place the next corridor piece
    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        int distance = int.MaxValue;
        foreach(var position in roomCenters)
        {
            //distance from one of the room centre positions to the current room centre
            int currentDistance = Mathf.Abs(position.x - currentRoomCenter.x) + Mathf.Abs(position.y - currentRoomCenter.y);
            //if the current distance is smaller then distance
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
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

    private void FindBottomLeftRoom(List<BoundsInt> roomsList)
    {
        Vector3 smallestRoomCentre = new Vector3(int.MaxValue, int.MaxValue, 0);
        foreach (var room in roomsList)
        {
            if (room.center.magnitude <= smallestRoomCentre.magnitude)
            {
                smallestRoomCentre = room.center;
            }
        }
        Vector3Int SmallesRoomCentreRoundedToInt = Vector3Int.RoundToInt(smallestRoomCentre);
        startDoor.transform.position = addCenteringOffset(SmallesRoomCentreRoundedToInt);
        playerAgent.transform.position = startDoor.transform.position;
    }

    private void FindTopRightRoom(List<BoundsInt> roomsList)
    {
        Vector3 largestRoomCentre = new Vector3(0, 0, 0);
        foreach (var room in roomsList)
        {
            if (room.center.magnitude >= largestRoomCentre.magnitude)
            {
                largestRoomCentre = room.center;
            }
        }
        Vector3Int LargestRoomCentreRoundedToInt = Vector3Int.RoundToInt(largestRoomCentre);
        endDoor.transform.position = addCenteringOffset(LargestRoomCentreRoundedToInt);
    }

    private Vector3 addCenteringOffset(Vector3 startingVector)
    {
        Vector3 returnVector = new Vector3(startingVector.x + 0.5f, startingVector.y + 0.5f, startingVector.z);
        return returnVector;
    }
}
