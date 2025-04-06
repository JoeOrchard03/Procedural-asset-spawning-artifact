using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/***************************************************************************************
*    Title: Custom Inspector - P7 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 19/12/2020
*    Availability: https://www.youtube.com/watch?v=U3Wr-sNnJNk
***************************************************************************************/

//Create custom editor as to allow the creation of a custom button to test dungeon in editor
[CustomEditor(typeof(SCR_AbstractDungeonGen), true)]
public class SCR_RandomDungeonGenEditor : Editor
{
    SCR_AbstractDungeonGen generator;

    private void Awake()
    {
        generator = (SCR_AbstractDungeonGen)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //Creates a button and runs if it is pressed
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon(true);
        }
    }
}
