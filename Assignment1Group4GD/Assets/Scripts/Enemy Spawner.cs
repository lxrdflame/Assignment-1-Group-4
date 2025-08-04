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
            GameObject enemy2 = Instantiate(Enemies[0], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnRate);
        }

        
        yield return new WaitForSeconds(10);
        for (int P = 0; P < EnemiesToSpawn; P++)
        {
            Debug.Log("nEW eNEMIES");
            GameObject enemy = Instantiate(Enemies[Random.Range(0,1)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            GameObject enemy2 = Instantiate(Enemies[Random.Range(0,1)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnRate);
        }

        yield return new WaitForSeconds(300);
        for (int L = 0; L < EnemiesToSpawn; L ++)
        {
            GameObject enemy = Instantiate(Enemies[Random.Range(1, 2)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            GameObject enemy2 = Instantiate(Enemies[Random.Range(1, 2)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnRate);
        }
    }
}
