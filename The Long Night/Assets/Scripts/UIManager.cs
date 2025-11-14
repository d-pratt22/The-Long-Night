using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject menu;

    public GameObject inventory;
    public InventoryManager inventoryManager;

    public GameObject characterSheet;

    public UnityEngine.UI.Button switchMenuButton;

    public GameObject interactText;

    public float menuWaitTime = 1f;

    private bool menuEnabled = false;
    private bool inventoryEnabled = false;
    private bool sheetEnabled = false;
    private bool canToggle = true;

    private void Awake()
    {
        menu.SetActive(false);

        interactText.SetActive(false);

        switchMenuButton.onClick.AddListener(SwitchMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canToggle)
        {
            menuEnabled = !menuEnabled;
            menu.SetActive(menuEnabled);

            ToggleInventory();
        }
    }

    private void SwitchMenu()
    {
        sheetEnabled = !sheetEnabled;

        if (sheetEnabled)
        {

        }

        characterSheet.SetActive(sheetEnabled);
        StartCoroutine(MenuWait());
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

    public void ToggleInteractButton(bool interactOn)
    {
        interactText.SetActive(interactOn);
    }

    IEnumerator MenuWait()
    {
        canToggle = false;
        yield return new WaitForSeconds(menuWaitTime);
        canToggle = true;
    }
}
