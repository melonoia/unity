using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public GameObject[] players;
    public static GameObject player;
    public int selectedPlayer = 0;
    // Start is called before the first frame update

    void Start()
    {
        foreach (GameObject pl in players)
        {
            pl.SetActive(false);
        }
        players[selectedPlayer].SetActive(true);
        player = players[selectedPlayer];
    }

    public void ChangePlayer(int newPlayer)
    {
        players[selectedPlayer].SetActive(false);
        players[newPlayer].SetActive(true);
        selectedPlayer = newPlayer;
    }
}
