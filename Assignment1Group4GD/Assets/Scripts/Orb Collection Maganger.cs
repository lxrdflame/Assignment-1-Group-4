using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCollectionMaganger : MonoBehaviour
{
    public Slider OrbCollections;
    [SerializeField]
    private int OrbsCollected;
    private int orbscollectedMax = 15; //Delete if needed
    public GameObject UpgradePanel, UpgradePanel2;

    [SerializeField] private UIManager uiManager; // can delete
    

    private void Update()
    {
        OrbCollections.value = OrbsCollected;
        OrbCollections.maxValue = orbscollectedMax; // delete if needed
        if (OrbsCollected == orbscollectedMax)
        {
            Time.timeScale = 0;
            OrbsCollected -= orbscollectedMax; // Delete if necessary
            if(orbscollectedMax != 30)
            {
                orbscollectedMax += 15; // change to orbscollected -= 15 if game is not working;
            }
            uiManager.OpenUpgradeMenu();
            UpgradePanel.SetActive(true);
            UpgradePanel2.SetActive(true);
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
