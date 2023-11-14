using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private bool canCount = true;

    public void AddScore(int amount)
    {
        if (canCount)
        {
            score += amount;
            UpdateScore();
        }
    }

    public void StopCountingForSeconds(float seconds)
    {
        StartCoroutine(StopCountingRoutine(seconds));
    }

    private IEnumerator StopCountingRoutine(float seconds)
    {
        canCount = false;
        yield return new WaitForSeconds(seconds);
        canCount = true;
    }

    private void UpdateScore()
    {
        scoreText.text = "M: " + score.ToString();
    }
}
