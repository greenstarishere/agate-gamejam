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

    enum animState { idle, changing}
    animState currentState;

    float score;
    float maxScore;
    float lerpedScore;

    public int totalBranch;
    public ProgressBar progressBar;
    public GameObject timer;


    // Start is called before the first frame update
    private void Start()
    {
        maxScore = totalBranch * 10;
    }

    private void Update()
    {
        switch (currentState)
        {
            case animState.idle:
                break;
            case animState.changing:
                displayScore();
                break;
        }
    }
    public void addScore(int points)
    {
        Timers timerComponent = timer.GetComponent<Timers>();
        timerComponent.StopTimer();
        timerComponent.Reset();
        timer.SetActive(false);
        lerpedScore = score;
        score += points;
        ConversationManager.Instance.SetInt("finalScore", Mathf.CeilToInt(score / maxScore * 100));
        currentState = animState.changing;
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
        lerpedScore = Mathf.Lerp(lerpedScore, score, 5 * Time.deltaTime);
        float calcScore = lerpedScore / maxScore * 100;
        progressBar.SetValue(calcScore);
    }
}
