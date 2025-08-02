using System.Collections.Generic;
using UnityEngine;

public class UpgradeRandomizer : MonoBehaviour
{
    [Header("Upgrade Options")]
    [SerializeField] private List<Rogueitems> availableItems; // All possible Rogueitems ScriptableObjects
    [SerializeField] private Itempurchase[] upgradeButtons;   // Buttons using Itempurchase script
    [SerializeField] private Transform itemSpawnPoint;        // Where the selected item will spawn

    private void OnEnable()
    {
        AssignRandomUpgrades();
    }

    public void AssignRandomUpgrades()
    {
        List<Rogueitems> chosenItems = new List<Rogueitems>();

        // Randomly pick unique items from the list
        while (chosenItems.Count < upgradeButtons.Length && chosenItems.Count < availableItems.Count)
        {
            Rogueitems randomItem = availableItems[Random.Range(0, availableItems.Count)];
            if (!chosenItems.Contains(randomItem))
            {
                chosenItems.Add(randomItem);
            }
        }

        // Assign items to each button via the Itempurchase script
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (i < chosenItems.Count)
            {
                upgradeButtons[i].Initialize(chosenItems[i], itemSpawnPoint);
            }
            else
            {
                Debug.LogWarning($"Not enough items to assign to button index {i}");
            }
        }
    }
}
