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
    [SerializeField] float GScoreTotal = 0, GScore = 0, HScore, FScore;
    [SerializeField] Vector3 StartNodePositon, EndGoalPosition;
    [SerializeField] TMP_Text GScoreText, HScoreText, FScoreText;    

    public class NodeBase
    {
        public NodeBase Connection { get; private set; }

    }

    public void SetPlayerReference()
    {   if(playerReference!= null) { Debug.Log("player reference already set"); return; }
        playerReference = GameObject.FindGameObjectWithTag("PlayerAgent");
        StartNodePositon = GameObject.FindGameObjectWithTag("StartNode").transform.position;
        EndGoalPosition = GameObject.FindGameObjectWithTag("EndNode").transform.position;
    }

    public void CalculatePathScores()
    {
        CalculateGScore();
        CalculateHScore();
        CalculateFScore();
        UpdateScoreText();
        Debug.Log("GScore is: " + GScoreTotal + " HScore is: " + HScore + " FScore is: " + FScore);
    }

    public void ResetGScoreTotal()
    {
        GScoreTotal = 0;
    }

    private void UpdateScoreText()
    {
        GScoreText.text = "G: " + GScoreTotal;
        HScoreText.text = "H: " + HScore;
        FScoreText.text = "F: " + FScore;
    }

    private void CalculateGScore()
    {
        GScore = Mathf.Abs(gameObject.transform.position.x - StartNodePositon.x) + Mathf.Abs(gameObject.transform.position.y - StartNodePositon.y);
        GScoreTotal += GScore;
    }

    private void CalculateHScore()
    {
        HScore = Mathf.Abs(gameObject.transform.position.x - EndGoalPosition.x) + Mathf.Abs(gameObject.transform.position.y - EndGoalPosition.y);
    }

    private void CalculateFScore()
    {
        FScore = GScoreTotal + HScore;
    }
}
