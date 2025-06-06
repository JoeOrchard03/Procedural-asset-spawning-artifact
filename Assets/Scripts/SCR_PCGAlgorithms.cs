using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Random Walk Algorithm - P4 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 18/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=F_Zc1nvtB0o&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=4
*    Availaibility git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/ProceduralGenerationAlgorithms.cs
***************************************************************************************/

public static class SCR_PCGAlgorithms
{
    //Random walk for creating rooms algorithm is stored as a hash set as hash sets store unique values
    public static HashSet<Vector2Int> RandomWalk(Vector2Int StartPos, int walkLength)
    {
        HashSet<Vector2Int> returnData = new HashSet<Vector2Int>();

        returnData.Add(StartPos);

        var previousPos = StartPos;

        //Iterates through for every element in walk length, walks a new step in a random diretion and adds it to the return data
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPos + Direction2D.GetRandomDirection();
            returnData.Add(newPosition);
            previousPos = newPosition;
        }
        return returnData;
    }

    //Random walk algorithm for creating corridors - using List to access positions in order so can start the next corridor from the previous one's end point
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomDirection();
        var currentPos = startPosition;
        //Store the starting position
        corridor.Add(currentPos);
        for (int i = 0; i< corridorLength;i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }

    //Splits space up randomly into rectangular rooms
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSpilt, int minWidth, int minHeight)
    {
        //Queue is like an array or list, used to store objects
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        //Enque adds an item to the back of the queue
        roomsQueue.Enqueue(spaceToSpilt);
        //While there are items in the queue
        while(roomsQueue.Count > 0)
        {
            //Get ready to handle a room by taking it from the front of the queue
            var room = roomsQueue.Dequeue();
            //If rooms are big enough to be split
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                //Random is used to randomise whether they are to be checked whether it can be split horizontally or vertically first
                if(Random.value < 0.5f)
                {
                    //If the room can fit another two rooms under the minimum height limit
                    if(room.size.y >= minHeight*2)
                    {
                        SplitHorizontally(minWidth, roomsQueue, room);
                    }
                    //If the room can fit another two rooms under the minimum width limit
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minHeight, roomsQueue, room);
                    }
                    else
                    {
                        //If it cant be split add it to the list of rooms
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minHeight, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, roomsQueue, room);
                    }
                    else
                    {
                        //If it cant be split add it to the list of rooms
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitHorizontally(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        //Defines where to split the room in a random range from the beginning of the room to it's max size
        var ySplit = Random.Range(1, room.size.y);
        //First room origin starts at the bottom left of the room and goes up to its x value and stops at where it is split on the y axis
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        //Second room origin starts at the bottom left of the first room by adding ySplit to the room.min, ySplit is taken from room.size.y so it captures the remaining space
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        //Defines where to split the room in a random range from the beginning of the room to it's max size
        var xSplit = Random.Range(1, room.size.x);
        //First room origin starts at the bottom left of the room and goes up to its y value and stops at where it is split on the x axis
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        //Second room origin starts at the bottom left of the first room by adding xSplit to the room.min, xSplit is taken from room.size.x so it captures the remaining space
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

//Class for access to simple direction data
public static class Direction2D
{
    //Cardinal directions
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(1, 1), //UP-RIGHT
        new Vector2Int(1, -1), //RIGHT-DOWN
        new Vector2Int(-1, -1), //DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 1), //UP-RIGHT
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(1, -1), //RIGHT-DOWN
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, -1), //DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static Vector2Int GetRandomDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}