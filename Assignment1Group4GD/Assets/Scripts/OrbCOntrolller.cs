using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCOntrolller : MonoBehaviour
{
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance <= 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 50 * Time.deltaTime);
        }
    }
}
