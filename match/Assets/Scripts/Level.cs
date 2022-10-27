using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public enum LevelType
    {
        TIMER,
        OBSTACLE,
        MOVES,
        FRUIT,
    };

    public Grid grid;
    public HUD hud;

    public int score1Star;
    public int score2Star;
    public int score3Star;

    protected LevelType type;

    public LevelType Type
    {
        get { return type; }
    }

    protected int currentScore;
    protected bool didWin;


    // Start is called before the first frame update
    void Start()
    {
        hud.SetScore(currentScore);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void GameWin()
    {
        grid.music.Stop();
        grid.music.PlayOneShot(grid.clips[3], 1.0f);
        grid.GameOver();
        didWin = true;
        StartCoroutine(WaitForGridFill());
    }

    public virtual void GameLose()
    {
        grid.music.Stop();
        grid.music.PlayOneShot(grid.clips[4], 1.0f);
        grid.GameOver();
        didWin = false;
        StartCoroutine(WaitForGridFill());
    }

    public virtual void OnMove()
    {

    }

    public virtual void OnPieceCleared(GridPiece piece)
    {
        // Update Score

        currentScore += piece.score;
        hud.SetScore(currentScore);
    }

    protected virtual IEnumerator WaitForGridFill()
    {
        while (grid.IsFilling)
        {
            yield return 0;
        }

        if (didWin)
        {
            hud.OnGameWin(currentScore);
        }
        else
        {
            hud.OnGameLose();
        }
    }
}
