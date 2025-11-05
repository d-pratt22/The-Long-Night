using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject inventory;
    public InventoryManager inventoryManager;

    public float menuWaitTime = 1f;

    private bool inventoryEnabled = false;
    private bool canToggle = true;

    private void Awake()
    {
        inventory.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canToggle)
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        inventoryEnabled = !inventoryEnabled;
        if (inventoryEnabled)
        {
            inventoryManager.ListItems();
        }
        inventory.SetActive(inventoryEnabled);
        StartCoroutine(MenuWait());
    }

    IEnumerator MenuWait()
    {
        canToggle = false;
        yield return new WaitForSeconds(menuWaitTime);
        canToggle = true;
    }
}
