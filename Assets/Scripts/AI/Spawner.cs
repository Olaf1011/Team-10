using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] enemyType;
    GameObject enemyPrefab;

    private float timeDelta = 0;

    void Start()
    {
        timeDelta = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // Whilst timeDelta is above zero, we minus the time between the last update calls then check timeDelta again
        // if it is below zero we know the correct time has passed and we spawn an enemy from one of four random spawn
        // points. We then reset timeDelta to a random value.
        if (timeDelta > 0)
        {
            timeDelta -= Time.deltaTime;
            if (timeDelta <= 0)
            {
                int randomPoint = Random.Range(0, spawnPoints.Length);
                int enemyChosen = Random.Range(0, enemyType.Length);
                GameObject enemyPrefab = enemyType[enemyChosen];
                Vector3 pos = spawnPoints[randomPoint].position;
                GameObject enemy = Spawn(pos);

                timeDelta = Random.Range(minTime, maxTime);
            }
        }
    }

    public GameObject Spawn(Vector3 pos)
    {
        return Instantiate(enemyType[Random.Range(0, enemyType.Length)], pos, Quaternion.identity) as GameObject;

    }
}


