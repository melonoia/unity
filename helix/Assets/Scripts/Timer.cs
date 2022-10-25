using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private int timeInSeconds;
    private float timer;
    private bool timeOut = false;
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = string.Format("{0}:{1:00}", timeInSeconds / 60, timeInSeconds % 60);
        timeInSeconds = Random.Range(15, 35);
    }

    // Update is called once per frame
    public void Update()
    {

        if (!timeOut && GameManager.isGameStarted)
        {

            timer += Time.deltaTime;
            timerText.text = string.Format("{0}:{1:00}", (int)Mathf.Max((timeInSeconds - timer) / 60, 0), (int)Mathf.Max((timeInSeconds - timer) % 60, 0));


            if (timeInSeconds - timer <= 0)
            {
                timeOut = true;
                GameManager.gameOver = true;
            }
        }

    }
}
