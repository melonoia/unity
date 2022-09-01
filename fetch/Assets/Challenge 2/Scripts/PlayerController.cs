using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject dogPrefab;
    private bool spawn = false;

    void Start()
    {
        // let player spawn after 3 seconds
        InvokeRepeating("spawnDog", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && spawn == true)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            // Reset when to spawn
            count = false;
        }
    }
    public bool spawnDog()
    {
        return spawn = true;
    }
}
