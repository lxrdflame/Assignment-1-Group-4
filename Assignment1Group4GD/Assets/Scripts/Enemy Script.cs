using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class EnemyScript : MonoBehaviour
{
    public float HP;

    
    public GameObject Player;
    [SerializeField]
    private NavMeshAgent agent;
    public GameObject Explotion;
    public AudioSource ExplotionAudio;
    public GameObject BombExplotionGO;

    public GameObject Orb;
    HealthManager healthManager;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        healthManager = Player.GetComponent<HealthManager>();



    }

    private void Update()
    {

        if (agent != null )
        {
            agent.SetDestination(Player.transform.position);
        }

        if (HP <= 0)
        {
            Instantiate(Orb, transform.position, Quaternion.identity);
            GameObject ExplotionParticle = Instantiate(Explotion, transform.position, Quaternion.identity);
            healthManager.ExplotionAudio.Play();
            Destroy(ExplotionParticle, 3);
            Destroy(gameObject);
        }
    }
}
