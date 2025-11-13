using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public ItemAsset item;
    public PlayerShooting playerShooting;
    public PlayerHealth playerHealth;

    private void Awake()
    {
        if (playerShooting == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerShooting = player.GetComponent<PlayerShooting>();
                playerHealth = player.GetComponent<PlayerHealth>(); 
            }
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
        if (playerShooting == null || playerHealth == null)
        {
            Awake();
        }

        switch (item.itemType)
        {
            case ItemAsset.ItemType.Weapon:
                EquipWeapon();
                break;
            case ItemAsset.ItemType.Healing:
                HealingItem();
                RemoveItem();
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

    private void HealingItem()
    {
        if (item is  HealingItem healing)
        {
            playerHealth.UseHealingItem(healing);
        }
    }
}
