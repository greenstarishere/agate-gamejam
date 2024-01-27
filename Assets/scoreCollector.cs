using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class scoreCollector : MonoBehaviour
{
    int score;

    // Start is called before the first frame update
    public void addScore(int points)
    {
        score += points;
    }

    public int getScore()
    {
        return score;
    }

    public void debugScore()
    {
        Debug.Log(score);
    }
}
