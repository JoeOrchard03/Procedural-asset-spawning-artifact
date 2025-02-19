using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Saving parameters to scriptable object - P8 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=LRYfcTKpUhI&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=8
*    Avaliablility git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/Data/SimpleRandomWalkSO.cs
***************************************************************************************/

[CreateAssetMenu(fileName = "RandomWalkParams_", menuName = "PCG/RandomWalkData")]
public class SCR_RandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}                        
