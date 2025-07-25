using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 hitPoint;
    [SerializeField]
    private int MoveSpeed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hitPoint, MoveSpeed * Time.deltaTime);
    }
}
