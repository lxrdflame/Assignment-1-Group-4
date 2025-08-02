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
