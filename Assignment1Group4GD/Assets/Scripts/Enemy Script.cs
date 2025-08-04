using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public int HP;

    
    public GameObject Player;
    [SerializeField]
    private NavMeshAgent agent;


    public GameObject Orb;

    

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        if (agent != null)
        {
            agent.SetDestination(Player.transform.position);
        }

        if (HP <= 0)
        {
            Instantiate(Orb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
