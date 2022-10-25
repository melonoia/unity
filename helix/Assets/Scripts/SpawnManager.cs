using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private Rigidbody playerRB;
    //public Transform playerBox;
    public float bounceForce = 6;
    private AudioManager audioManager;
    // Start is called before the first frame update

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
        InvokeRepeating("SpawnCharacter", 3f, 5f);
    }

    private void SpawnCharacter()
    {
        if (GameManager.isGameStarted)
        {
            GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0, 2, -2), Quaternion.identity);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, playerRB.velocity.z);

        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

        if (materialName == "Safe (Instance)")
        {
            audioManager.Play("bounce");
        }
        else if (materialName == "Unsafe (Instance)")
        {
            GameManager.gameOver = true;
            audioManager.Play("game over");

        }
        else if (materialName == "LastRing (Instance)" && !GameManager.levelCompleted)
        {
            GameManager.levelCompleted = true;
            audioManager.Play("win level");
        }
    }

}
