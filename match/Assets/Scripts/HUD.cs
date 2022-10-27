using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    public Level level;
    public TextMeshProUGUI remainingText;
    public TextMeshProUGUI remainingSubText;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI targetSubText;
    public TextMeshProUGUI scoreText;
    public GameObject[] stars;
    private int starIdx = 0;
    public GameOver gameOver;
    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i == starIdx)
            {
                stars[i].SetActive(true);
            }
            else
            {
                stars[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
        int visibleStar = 0;

        if (score >= level.score1Star && score < level.score2Star)
        {
            visibleStar = 1;
        }
        else if (score >= level.score2Star && score < level.score3Star)
        {
            visibleStar = 2;
        }
        else if (score >= level.score3Star)
        {
            visibleStar = 3;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if (i == visibleStar)
            {
                stars[i].SetActive(true);
            }
            else
            {
                stars[i].SetActive(false);
            }
        }
        starIdx = visibleStar;
    }

    public void SetTarget(int target)
    {
        targetText.text = target.ToString();
    }

    public void SetRemaining(int remaining)
    {
        remainingText.text = remaining.ToString();
    }

    public void SetRemaining(string remaining)
    {
        remainingText.text = remaining;
    }

    public void SetLevelType(Level.LevelType type)
    {
        if (type == Level.LevelType.MOVES)
        {
            remainingSubText.text = "moves remaining";
            targetSubText.text = "target score";
        }
        else if (type == Level.LevelType.OBSTACLE)
        {
            remainingSubText.text = "moves remaining";
            targetSubText.text = "boxes left";
        }
        else if (type == Level.LevelType.TIMER)
        {
            remainingSubText.text = "time remaining";
            targetSubText.text = "target score";
        }
    }

    public void OnGameWin(int score)
    {

        gameOver.ShowWin(score, starIdx);
        if (starIdx > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, starIdx);
        }
    }

    public void OnGameLose()
    {

        gameOver.ShowLose();
    }
}
