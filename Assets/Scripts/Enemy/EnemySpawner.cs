using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public Transform[] spawnPoints;
    public int numberOfWaves = 3;
    public int numberOfEnemiesPerWave = 5;
    public float timeBetweenWaves = 10f;
    public float timeBetweenEnemies = 0.5f;
    public GameObject gate;
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 0; wave < numberOfWaves; wave++)
        {
            if (wave == 2)
            {
                gate.SetActive(true);
                GameObject playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
                GameController gameController = playerCanvas.GetComponent<GameController>();
                gameController.EnableDoorMsg();
            }

            for (int i = 0; i < numberOfEnemiesPerWave; i++)
            {
                for (int j = 0; j < Enemies.Length; j++)
                {
                    int spawnIndex = Random.Range(0, spawnPoints.Length);
                    GameObject enemy1 = Instantiate(Enemies[j], spawnPoints[spawnIndex].position, Quaternion.identity);
                    EnemyAttribute enemyAttribute1 = enemy1.GetComponent<EnemyAttribute>();
                    enemyAttribute1.IncreaseDamage(5 * wave);
                    enemyAttribute1.IncreaseExperience(5 * wave);
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            //increase enemy damage
            numberOfEnemiesPerWave += 2;
            //timeBetweenWaves -= 2;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}