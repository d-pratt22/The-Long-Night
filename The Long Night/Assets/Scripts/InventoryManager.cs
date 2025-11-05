using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemAsset> Items = new List<ItemAsset>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public InventoryItemController[] inventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemAsset item)
    {
        Items.Add(item);
    }

    public void Remove(ItemAsset item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemSprite = obj.transform.Find("itemImage").GetComponent<Image>();

            itemName.text = item.itemName;
            itemSprite.sprite = item.itemSprite;
        }

        SetInventoryItems();
    }

    public void SetInventoryItems()
    {
        inventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            inventoryItems[i].AddItem(Items[i]);
        }
    }
}
