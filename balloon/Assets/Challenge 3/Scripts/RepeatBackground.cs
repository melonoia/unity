using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public Vector3 startPos;
    private float repeatWidth;

    private void Start()
    {
        // Establish the default starting position 
        startPos = transform.position;

        // Set repeat width to half of the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    private void Update()
    {

        // If background moves left by its repeat width, move it back to start position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }

}
