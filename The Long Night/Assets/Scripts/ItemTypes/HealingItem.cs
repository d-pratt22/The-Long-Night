using UnityEngine;

[CreateAssetMenu(fileName = "NewHealing", menuName = "Inventory/Healing")]
public class HealingItem : ItemAsset
{
    public int healthRestored;

    private void OnEnable()
    {
        itemType = ItemType.Healing;
    }
}