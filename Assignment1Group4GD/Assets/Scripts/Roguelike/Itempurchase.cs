using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Itempurchase : MonoBehaviour

{
    private Rogueitems itemInfo;
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemText;
    public Transform spawnPoint;

    public void Initialize(Rogueitems newItem, Transform spawn)
    {
        itemInfo = newItem;
        spawnPoint = spawn;
        if (itemText != null)
        {
            itemText.text = itemText.text = $"<b>{itemInfo.itemName}</b>\n<size=75%>{itemInfo.description}</size>"; // or itemInfo.description if preferred
        }
        if (itemSprite != null && itemInfo != null)
        {
            itemSprite.sprite = itemInfo.itemSprite;
        }

     }

    public void BuyItem()
    {

            if (itemInfo == null) return;

    // Apply stat changes
    if (itemInfo.modifiesStats && itemInfo.statToModify != Rogueitems.StatType.None)
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
        {
            switch (itemInfo.statToModify)
            {
                case Rogueitems.StatType.Damage:
                    stats.ModifyStat("damage", itemInfo.statValue);
                    break;
                case Rogueitems.StatType.FireRate:
                    stats.ModifyStat("firerate", -itemInfo.statValue); // lower = faster
                    stats.fireRate = Mathf.Max(0.05f, stats.fireRate);
                    break;
                case Rogueitems.StatType.MoveSpeed:
                    stats.ModifyStat("movespeed", itemInfo.statValue);
                    break;
                case Rogueitems.StatType.JumpHeight:
                    stats.ModifyStat("jumpheight", itemInfo.statValue);
                    break;
                case Rogueitems.StatType.DescentSpeed:
                    stats.ModifyStat("descent", itemInfo.statValue);
                    break;
                case Rogueitems.StatType.MaxHealth:
                    stats.maxHealth += itemInfo.statValue;
                    stats.currentHealth += itemInfo.statValue;
                    break;
            }
        }
    }
        if (itemInfo != null && itemInfo.itemPrefab != null)
        {
            Instantiate(itemInfo.itemPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        if (UpgradePauseController.Instance != null)
        {
            UpgradePauseController.Instance.CloseUpgradeMenu();
        }
    }
}
