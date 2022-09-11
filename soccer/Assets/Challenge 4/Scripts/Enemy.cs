using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public SpawnManager enemyScript;
    private Rigidbody enemyRb;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.FindGameObjectWithTag("PlayerGoal");
        enemyScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
 		
    }

    // Update is called once per frame
    void Update()
    {
    	Debug.Log(enemyScript.enemySpeed);
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }

}
