using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int Health;
    public Slider HealthSlider;
    public GameObject Explotion;
    public GameObject GameOverScreen;
    public AudioSource ExplotionAudio;


    private void Start()
    {
            ExplotionAudio.Stop();

    }
    private void Update()
    {
        HealthSlider.value = Health;

        if (Health <= 0)
        {
            GameOverScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("WalkingEnemy"))
        {
            Health -= 5;
            Destroy(hit.gameObject);
            GameObject ExplotionParticle = Instantiate(Explotion, hit.transform.position, Quaternion.identity);
            ExplotionAudio.Play();
            Destroy(ExplotionParticle, 3);
        }
        else if (hit.CompareTag("EnemyBullet"))
        {
            Health -= 5;
        }
    }
}
