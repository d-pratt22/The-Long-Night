using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public WeaponItem equippedWeapon;
    private FixedCamera camScript;
    private bool canShoot = true;
    private int bulletCount = 0;

    private void Start()
    {
        camScript = Camera.main.GetComponent<FixedCamera>();
    }

    public void EquipWeapon(WeaponItem newWeapon)
    {
        equippedWeapon = newWeapon;

        if(equippedWeapon != null)
        {
            bulletCount = equippedWeapon.bulletCount;
        }
    }

    void Update()
    {
        if (camScript != null && camScript.isAiming && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {

        if (equippedWeapon == null)
        {
            Debug.LogWarning("No weapon equipped!");
            return;
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(canShoot && bulletCount > 0)
        {
            bulletCount--;
            
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
                Debug.Log($"Hit object: {hit.collider.name}");

                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(equippedWeapon.baseDamage, hit.point);
                }
            }

            StartCoroutine(AttackSpeed());
        }
    }

    IEnumerator AttackSpeed()
    {
        canShoot = false;
        yield return new WaitForSeconds(equippedWeapon.attackSpeed);
        canShoot = true;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(equippedWeapon.reloadSpeed);
        bulletCount = equippedWeapon.bulletCount;
    }
}
