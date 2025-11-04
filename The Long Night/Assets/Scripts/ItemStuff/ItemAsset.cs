using UnityEngine;

[CreateAssetMenu]

public class ItemAsset : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    [TextArea] public string itemText;
}
