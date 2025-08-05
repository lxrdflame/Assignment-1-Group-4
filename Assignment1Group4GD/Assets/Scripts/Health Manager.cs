using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int Health;
    public Slider HealthSlider;
    public GameObject Explotion;

    private void Update()
    {
        //HealthSlider.value = Health;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("WalkingEnemy"))
        {
            Health -= 5;
            Destroy(hit.gameObject);
            GameObject ExplotionParticle = Instantiate(Explotion, hit.transform.position, Quaternion.identity);
            Destroy(ExplotionParticle, 3);
        }
    }
}
