using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{

    void Update()
    {
      transform.Translate(Vector3.up * 6 * Time.deltaTime);
    }
}
