using UnityEngine;

public class RewardSpawn : MonoBehaviour
{
    public int maxCoin = 3;

    public float chanceToSpawn = 0.5f;

    private GameObject[] rewards;

    public bool forceSpawnAll = false;

    private void Awake()
    {

        rewards = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            rewards[i] = transform.GetChild(i).gameObject;
            // OnDisable();
        }
    }

    private void OnEnable()
    {
        if (Random.Range(.0f, 1.0f) > chanceToSpawn)
            return;

        if (forceSpawnAll)
        {
            int z = Random.Range(0, maxCoin);
            for (int i = 0; i < z; i++)
                rewards[i].SetActive(true);
        }
        else
        {
            int r = Random.Range(0, maxCoin);
            for (int i = 0; i < r; i++)
            {
                rewards[i].SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        foreach (GameObject go in rewards)
            if (go != null)
                go.SetActive(false);
            else
                Debug.Log("No list");
    }

}
