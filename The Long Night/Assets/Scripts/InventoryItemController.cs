using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public ItemAsset item;
    public PlayerShooting playerShooting;

    private void Awake()
    {
        if (playerShooting == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerShooting = player.GetComponent<PlayerShooting>();
        }
    }

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

    private void EquipWeapon()
    {
        Debug.Log("Weapon Equipped");

        if (item is WeaponItem weapon)
        {
            playerShooting.EquipWeapon(weapon);
        }
    }
}
