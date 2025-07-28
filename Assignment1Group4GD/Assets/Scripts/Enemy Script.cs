using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int HP;
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 3 * Time.deltaTime);
        }
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
