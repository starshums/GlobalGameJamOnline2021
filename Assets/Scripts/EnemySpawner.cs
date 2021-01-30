using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject skullminionEnemy;
    public float spawnInterval = 5f;
    public float counter;
    void Start()
    {
        counter = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = spawnInterval;
            SpawnEnemy(skullminionEnemy);
        }
    }
    void SpawnEnemy(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, transform.position, transform.rotation);
    }
}
