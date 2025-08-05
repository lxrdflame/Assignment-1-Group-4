using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    private GameObject Player;
    public List<Transform> ShootPoints;
    [SerializeField]
    private int ShootRate;
    public GameObject BulletPrefab;
    private bool isShooting;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if(distance <= 20)
        {
            if(!isShooting)
            {
                StartCoroutine(ShootPlayer());
            }
        }

        foreach(Transform t in ShootPoints)
        {
            t.transform.LookAt(Player.transform.position);
        }
    }

    IEnumerator ShootPlayer()
    {
        isShooting = true;
        for (int i = 0; i < ShootPoints.Count; i++)
        {
            GameObject Bullet = Instantiate(BulletPrefab, ShootPoints[i].position, Quaternion.identity);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.velocity = ShootPoints[i].forward * 60;
        }
        yield return new WaitForSeconds(ShootRate);
        isShooting = false;

    }
}
