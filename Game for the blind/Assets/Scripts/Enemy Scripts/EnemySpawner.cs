using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //Spawn Radius
    public float minRad, maxRad;
    
    //Spawn timer
    private bool canSpawnEnemy;
    public float spawnTimer;
    public float spawnRate;
    
    //Enemy
    public GameObject enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        canSpawnEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnEnemy)
        {
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        canSpawnEnemy = false;
        SpawnEnemy();
        
        yield return new WaitForSeconds(spawnTimer);

        spawnTimer -= spawnRate;
        canSpawnEnemy = true;
    }

    private void SpawnEnemy()
    {
        Vector3 randomPos = Random.onUnitSphere * Random.Range(minRad, maxRad);
        randomPos.y = 0;
        
        Instantiate(enemy, randomPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, minRad);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, maxRad);
    }
}
