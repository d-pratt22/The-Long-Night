using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemAsset item;

    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
