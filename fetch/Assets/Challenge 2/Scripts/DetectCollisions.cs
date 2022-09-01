using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Destroy ball and dog on collision
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
