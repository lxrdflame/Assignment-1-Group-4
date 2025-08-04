using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Upgrade/Item")]
public class Rogueitems : ScriptableObject
{
    public string itemName;
    public string description;
    public GameObject itemPrefab;
    public Sprite itemSprite;
    
    [Header("Stat Upgrade Info")]
    public bool modifiesStats = false;

    public enum StatType
    {
        None,
        Damage,
        FireRate,
        MoveSpeed,
        JumpHeight,
        DescentSpeed,
        MaxHealth
    }

    public StatType statToModify = StatType.None;
    public float statValue = 0f;
}
