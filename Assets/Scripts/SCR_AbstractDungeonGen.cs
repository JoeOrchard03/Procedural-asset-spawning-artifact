using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Custom Inspector - P7 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability youtube tutorial link: https://www.youtube.com/watch?v=U3Wr-sNnJNk
*    Availability git link: https://github.com/SunnyValleyStudio/Unity_2D_Procedural_Dungoen_Tutorial/blob/main/_Scripts/AbstractDungeonGenerator.cs
***************************************************************************************/

public abstract class SCR_AbstractDungeonGen : MonoBehaviour
{
    [SerializeField]
    public SCR_TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProcGen();
    }

    public void AgentStep()
    {
        PathFindingAgentStep();
    }

    protected abstract void RunProcGen();

    protected abstract void PathFindingAgentStep();

}
