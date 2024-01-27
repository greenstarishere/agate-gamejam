using DialogueEditor;
using Michsky.UI.Heat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

[ExecuteInEditMode]
public class scoreCollector : MonoBehaviour
{
    float score;
    float maxScore;

    public int totalBranch;
    public ProgressBar progressBar;

    // Start is called before the first frame update
    private void Start()
    {
        maxScore = totalBranch * 10;
    }
    public void addScore(int points)
    {
        score += points;
        displayScore();
    }

    public float getScore()
    {
        return score;
    }

    public void debugScore()
    {
        Debug.Log(score);
    }

    public void displayScore()
    {
        float calcScore = score / maxScore * 100;
        progressBar.SetValue(calcScore);
    }
}
