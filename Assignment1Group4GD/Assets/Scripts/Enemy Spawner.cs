using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<Transform> SpawnPoints;
    [SerializeField]
    private int SpawnRate, EnemiesToSpawn;

    public List<GameObject> BossGlasses;
    public Rigidbody BossRigidBody;
    public GameObject Boss;
    public Transform StartPosition;
    public NavMeshAgent BossNav;

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

        foreach(GameObject Glass in BossGlasses)
        {
            Glass.SetActive(false);
        }
        BossGlasses[1].SetActive(true);

        yield return new WaitForSeconds(10);
        for (int P = 0; P < EnemiesToSpawn; P++)
        {
            Debug.Log("nEW eNEMIES");
            GameObject enemy = Instantiate(Enemies[Random.Range(1,1)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnRate);
        }

        foreach (GameObject Glass in BossGlasses)
        {
            Glass.SetActive(false);
        }
        BossGlasses[2].SetActive(true);

        yield return new WaitForSeconds(10);
        for (int L = 0; L < EnemiesToSpawn; L ++)
        {
            GameObject enemy2 = Instantiate(Enemies[Random.Range(2, 2)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnRate);
        }

        foreach (GameObject Glass in BossGlasses)
        {
            Glass.SetActive(false);
        }
        yield return new WaitForSeconds(30);


        BossNav.enabled = false;
        Boss.transform.position = StartPosition.position;
        BossRigidBody.isKinematic = false;
        BossNav.enabled = true;
    }
}
