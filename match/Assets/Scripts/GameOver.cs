using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject screenParent;
    public GameObject scoreParent;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI scoreText;
    public GameObject[] stars;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        screenParent.SetActive(false);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
    }

    public void ShowLose()
    {
        screenParent.SetActive(true);
        scoreParent.SetActive(false);

        Animator animator = GetComponent<Animator>();

        if (animator)
        {
            animator.Play("GameOverShow");
        }

    }

    public void ShowWin(int score, int starCount)
    {
        screenParent.SetActive(true);
        loseText.enabled = false;

        scoreText.text = score.ToString();
        scoreText.enabled = false;

        if (animator)
        {
            animator.Play("GameOverShow");
        }
        StartCoroutine(ShowWinCoroutine(starCount));

    }

    private IEnumerator ShowWinCoroutine(int starCount)
    {
        yield return new WaitForSeconds(0.5f);

        if (starCount < stars.Length)
        {
            for (int i = 0; i <= starCount; i++)
            {
                stars[i].SetActive(true);
                if (i > 0)
                {
                    stars[i - 1].SetActive(false);
                }
                yield return new WaitForSeconds(.5f);
            }
        }
        scoreText.enabled = true;
    }

    public void OnReplayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnDoneClicked()
    {
        SceneManager.LoadScene("LevelSelect");
    }

}
