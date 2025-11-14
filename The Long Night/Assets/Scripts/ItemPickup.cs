using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemAsset item;
    public UIManager uiManager;

    private bool inRange = false;

    public void Pickup()
    {
        uiManager.ToggleInteractButton(false);
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            uiManager.ToggleInteractButton(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            uiManager.ToggleInteractButton(false);
        }
    }

    private void Update()
    {

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }

    }
}
