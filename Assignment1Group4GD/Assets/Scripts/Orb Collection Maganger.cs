using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCollectionMaganger : MonoBehaviour
{
    public Slider OrbCollections;
    private int OrbsCollected;
    public GameObject UpgradePanel;

    private void Update()
    {
        OrbCollections.value = OrbsCollected;
        if (OrbsCollected == 15)
        {
            Time.timeScale = 0;
            OrbsCollected -= 15;
            UpgradePanel.SetActive(true);
        }
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
