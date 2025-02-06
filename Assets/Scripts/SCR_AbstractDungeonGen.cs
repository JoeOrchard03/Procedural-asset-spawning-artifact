using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Custom Inspector - P7 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability: https://www.youtube.com/watch?v=U3Wr-sNnJNk
***************************************************************************************/

public abstract class SCR_AbstractDungeonGen : MonoBehaviour
{
    [SerializeField]
    protected SCR_TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProcGen();
    }

    protected abstract void RunProcGen();

}
