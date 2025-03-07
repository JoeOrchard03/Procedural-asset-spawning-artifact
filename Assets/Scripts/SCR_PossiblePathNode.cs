using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SCR_PossiblePathNode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playerReference;
    [SerializeField] float GScore, HScore, FScore;
    [SerializeField] Vector3 StartNodePositon, EndGoalPosition;
    [SerializeField] TMP_Text GScoreText, HScoreText, FScoreText;

    public void SetPlayerReference(GameObject playerObj)
    {
        playerReference = playerObj;
    }

    public void CalculatePathScores()
    {
        CalculateGScore();
        CalculateHScore();
        CalculateFScore();
        UpdateScoreText();
        Debug.Log("GScore is: " + GScore + " HScore is: " + HScore + " FScore is: " + FScore);
    }

    private void UpdateScoreText()
    {
        GScoreText.text = "G: " + GScore;
        HScoreText.text = "H: " + HScore;
        FScoreText.text = "F: " + FScore;
    }

    private void CalculateGScore()
    {
        GScore = (gameObject.transform.position - StartNodePositon).magnitude;
    }

    private void CalculateHScore()
    {
        HScore = (gameObject.transform.position - EndGoalPosition).magnitude;
    }

    private void CalculateFScore()
    {
        FScore = GScore + HScore;
    }
}
