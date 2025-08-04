using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int HP;

    
    public GameObject Player;
    [SerializeField]
    private int Speed;

    
    public int AmountofOrbs; // can delete
    public GameObject Orb;

    

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        transform.LookAt(Player.transform.position);
        
        if (HP <= 0)
        {
            Instantiate(Orb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
