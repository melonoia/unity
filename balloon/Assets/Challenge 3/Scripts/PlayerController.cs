using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;
    private bool isLowEnough;
    private float topLimit = 10.5f;

    public float floatForce;
    private float gravityModifier = 1.0f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    public MeshRenderer playerMesh;

    private AudioSource playerAudio;
    public AudioSource cameraSound;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip balloonSound;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        cameraSound = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerMesh = GetComponent<MeshRenderer>();
        playerMesh.enabled = true;

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 1, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        isLowEnough = true;
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
            // Debug.Log("Space pressed!");
        }
        else
        {
            playerRb.AddForce(Vector3.down * floatForce);
        }

        if (playerRb.transform.position.y > topLimit)
        {
            isLowEnough = false;
            playerRb.AddForce(Vector3.down * 3);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Destroy(other.gameObject);
            playerMesh = false;
            cameraSound.Stop();
            Debug.Log("Game Over!");

        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
        // if player collides with ground, bounce
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerAudio.PlayOneShot(balloonSound, 1.0f);
            isLowEnough = true;
            playerRb.AddForce(Vector3.up * 3, ForceMode.Impulse);
        }
    }
}
