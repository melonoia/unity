using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickEvents : MonoBehaviour
{

    public TextMeshProUGUI soundsText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.mute)
            soundsText.text = "/";
        else
            soundsText.text = "";

    }

    public void ToggleMute()
    {
        if (GameManager.mute)
        {
            GameManager.mute = false;
            soundsText.text = "";
        }
        else
        {
            GameManager.mute = true;
            soundsText.text = "/";
        }
    }

    public void EasyButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Level");
    }

    public void TimerButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("TimerLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
