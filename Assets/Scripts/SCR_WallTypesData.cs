using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Placing Basic Walls - P9 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=LdpItlRg8OM&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=10
*    Avaliablility git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/WallTypesHelper.cs
***************************************************************************************/

/// <summary>
/// The wall types are stored as binary values, the numbers that contain four values are the neighbours in the cardinal directions starting with the up direction and going clockwise.
/// The numbers that have 8 directions include the neighbours in the cardinal and diagonal directions also starting with the up direction and going clockwise
/// </summary>
public static class SRC_WallTypesData
{
    public static HashSet<int> wallTop = new HashSet<int>
    {
        0b1111,
        0b0110,
        0b0011,
        0b0010,
        0b1010,
        0b1100,
        0b1110,
        0b1011,
        0b0111
    };

    public static HashSet<int> wallSideLeft = new HashSet<int>
    {
        0b0100
    };

    public static HashSet<int> wallSideRight = new HashSet<int>
    {
        0b0001
    };

    public static HashSet<int> wallBottom = new HashSet<int>
    {
        0b1000
    };

    public static HashSet<int> wallInnerCornerDownLeft = new HashSet<int>
    {
        0b11110001,
        0b11100000,
        0b11110000,
        0b11100001,
        0b10100000,
        0b01010001,
        0b11010001,
        0b01100001,
        0b11010000,
        0b01110001,
        0b00010001,
        0b10110001,
        0b10100001,
        0b10010000,
        0b00110001,
        0b10110000,
        0b00100001,
        0b10010001
    };

    public static HashSet<int> wallInnerCornerDownRight = new HashSet<int>
    {
        0b11000111,
        0b11000011,
        0b10000011,
        0b10000111,
        0b10000010,
        0b01000101,
        0b11000101,
        0b01000011,
        0b10000101,
        0b01000111,
        0b01000100,
        0b11000110,
        0b11000010,
        0b10000100,
        0b01000110,
        0b10000110,
        0b11000100,
        0b01000010

    };

    public static HashSet<int> wallDiagonalCornerDownLeft = new HashSet<int>
    {
        0b01000000
    };

    public static HashSet<int> wallDiagonalCornerDownRight = new HashSet<int>
    {
        0b00000001
    };

    public static HashSet<int> wallDiagonalCornerUpLeft = new HashSet<int>
    {
        0b00010000,
        0b01010000,
    };

    public static HashSet<int> wallDiagonalCornerUpRight = new HashSet<int>
    {
        0b00000100,
        0b00000101
    };

    public static HashSet<int> wallFull = new HashSet<int>
    {
        0b1101,
        0b0101,
        0b1101,
        0b1001

    };

    public static HashSet<int> wallFullEightDirections = new HashSet<int>
    {
        0b00010100,
        0b11100100,
        0b10010011,
        0b01110100,
        0b00010111,
        0b00010110,
        0b00110100,
        0b00010101,
        0b01010100,
        0b00010010,
        0b00100100,
        0b00010011,
        0b01100100,
        0b10010111,
        0b11110100,
        0b10010110,
        0b10110100,
        0b11100101,
        0b11010011,
        0b11110101,
        0b11010111,
        0b11010111,
        0b11110101,
        0b01110101,
        0b01010111,
        0b01100101,
        0b01010011,
        0b01010010,
        0b00100101,
        0b00110101,
        0b01010110,
        0b11010101,
        0b11010100,
        0b10010101

    };

    public static HashSet<int> wallBottmEightDirections = new HashSet<int>
    {
        0b01000001
    };

}
