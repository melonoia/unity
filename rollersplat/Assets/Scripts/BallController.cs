using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 15;

    private bool isTraveling;

    public int minSwipeRecognition = 500;

    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;
    private Vector2 swipePosCurrentFrame;
    private Vector2 swipePosLastFrame;
    private Vector2 currentSwipe;

    private Color solveColor;
    private Color ballColor;

    public ParticleSystem dash;

    private AudioSource gameMusic;
    private AudioSource wallSound;
    public AudioClip wallHit;

    // Start is called before the first frame update
    private void Start()
    {
        solveColor = Random.ColorHSV(.5f, 1);
        ballColor = Random.ColorHSV(2, 5);
        GetComponent<MeshRenderer>().material.color = ballColor;

        gameMusic = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        wallSound = GameObject.Find("BallPrefab").GetComponent<AudioSource>();
        gameMusic.Play();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (isTraveling)
        {
            rb.velocity = travelDirection * speed;
            dash.Play();
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), .05f);

        int i = 0;
        while (i < hitColliders.Length)
        {
            GroundPiece ground = hitColliders[i].GetComponent<GroundPiece>();
            if (ground && !ground.isColored)
            {
                ground.ChangeColor(solveColor);
            }
            i++;
        }

        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollisionPosition) < 1)
            {
                isTraveling = false;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
                wallSound.PlayOneShot(wallHit, 1.0f);
            }
        }

        if (isTraveling)
            return;

        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLastFrame != Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                if (currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();

                // up / down
                if (currentSwipe.x > -.5f && currentSwipe.x < .5f)
                {
                    // go up/down
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }

                if (currentSwipe.y > -.5f && currentSwipe.y < .5f)
                {
                    // go left/right
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }

            }
            swipePosLastFrame = swipePosCurrentFrame;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
            dash.Stop();
        }
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollisionPosition = hit.point;
        }
        isTraveling = true;
    }
}
