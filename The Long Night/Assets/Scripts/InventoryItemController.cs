using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public ItemAsset item;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(ItemAsset newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case ItemAsset.ItemType.Weapon:
                EquipWeapon();
                break;
        }

        //Maybe: RemoveItem();
    }

    public void EquipWeapon()
    {
        Debug.Log("Weapon Equipped");
    }
}
