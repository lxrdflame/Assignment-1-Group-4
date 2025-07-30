using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<Transform> SpawnPoints;
    [SerializeField]
    private int SpawnRate, EnemiesToSpawn;


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < EnemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(Enemies[0], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnRate);
        }
    }
}
