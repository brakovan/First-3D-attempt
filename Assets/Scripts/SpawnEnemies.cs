using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    private GameObject enemyPrefab;
    private GameObject[] enemyPool;
    private Vector3 spawnPos;
    

    [SerializeField]
    private int spawnRadiusEnemies, firstNumPosition, lastNumPosition;
    [SerializeField]
    private int numberEnemies;
    [SerializeField]
    private float spawnFreq;
    private float rndPosX;
    private float rndPosZ;
    private float rndPosMultiply;



    private void Start()
    {
        StartCoroutine(Spawned());
    }

    private void Awake()
    {
        
        enemyPool = new GameObject[numberEnemies];

        for (int i = 0; i < enemyPool.Length; i++)
            enemyPool[i] = Instantiate(enemyPrefab);

    }

    private IEnumerator Spawned()
    {
        while (true)
        {
            rndPosMultiply = UnityEngine.Random.Range(0, 2) * 2 - 1;
            rndPosX = UnityEngine.Random.Range(firstNumPosition, lastNumPosition);
            rndPosZ = Mathf.Sqrt(Mathf.Abs(spawnRadiusEnemies * spawnRadiusEnemies - rndPosX * rndPosX));
            rndPosZ *= rndPosMultiply;

            Spawn();

            yield return new WaitForSecondsRealtime(spawnFreq);

        }
    }

    private void Spawn()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (enemyPool[i].activeInHierarchy)
                continue;

            spawnPos = new Vector3(playerTransform.position.x + rndPosX,
                playerTransform.position.y,
                playerTransform.position.z + rndPosZ);

            enemyPool[i].SetActive(true);
            enemyPool[i].transform.position = spawnPos;

            break;
        }   

    }
}
