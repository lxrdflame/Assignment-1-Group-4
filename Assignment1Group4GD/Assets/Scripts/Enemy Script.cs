using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public int HP;

    
    public GameObject Player;
    [SerializeField]
    private int Speed;
    private NavMeshAgent agent;


    public int AmountofOrbs; // can delete
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
            agent.stoppingDistance = 0.1f;
        }

        if (HP <= 0)
        {
            Instantiate(Orb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
