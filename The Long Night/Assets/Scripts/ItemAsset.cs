using UnityEngine;

[CreateAssetMenu]

public class ItemAsset : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    [TextArea] public string itemText;
    public ItemType itemType;

    public enum ItemType
    {
        Weapon
    }
}
