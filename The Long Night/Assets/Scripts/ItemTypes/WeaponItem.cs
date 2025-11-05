using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponItem : ItemAsset
{
    public int baseDamage;
    public float attackSpeed;
    public float reloadSpeed;
    public int bulletCount;

    private void OnEnable()
    {
        itemType = ItemType.Weapon;
    }
}
