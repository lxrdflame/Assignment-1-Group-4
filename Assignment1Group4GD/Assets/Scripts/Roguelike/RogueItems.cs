using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Upgrade/Item")]
public class Rogueitems : ScriptableObject
{
    public string itemName;
     public string description;
    public GameObject itemPrefab;
    public Sprite itemSprite;
}
