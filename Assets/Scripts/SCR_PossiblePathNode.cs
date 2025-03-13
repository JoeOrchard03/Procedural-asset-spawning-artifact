using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using _Scripts.Tiles;

public class SCR_PossiblePathNode : SCR_NodeBase
{
    [SerializeField] TMP_Text GScoreText, HScoreText, FScoreText;    

    private void UpdateScoreText()
    {
        GScoreText.text = "G: " + G;
        HScoreText.text = "H: " + H;
        FScoreText.text = "F: " + F;
    }
}
