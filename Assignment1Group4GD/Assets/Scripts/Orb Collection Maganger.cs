using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCollectionMaganger : MonoBehaviour
{
    public Slider OrbCollections;
    private int OrbsCollected;


    private void Update()
    {
        OrbCollections.value = OrbsCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Orb"))
        {
            OrbsCollected++;
            Destroy(other.gameObject);
        }
    }
}
