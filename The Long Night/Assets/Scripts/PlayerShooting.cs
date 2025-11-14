using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public WeaponItem equippedWeapon;
    private FixedCamera camScript;
    private bool canShoot = true;
    private int bulletCount = 0;

    public TextMeshProUGUI bulletText;
    public GameObject reloadingText;

    public Transform weaponHolder;
    public List<WeaponVisual> weaponVisuals = new List<WeaponVisual>();



    private void Start()
    {
        camScript = Camera.main.GetComponent<FixedCamera>();

        foreach (var visual in weaponVisuals)
        {
            visual.weaponObject.SetActive(false);
        }
    }

    public void EquipWeapon(WeaponItem newWeapon)
    {
        equippedWeapon = newWeapon;

        if(equippedWeapon != null)
        {
            bulletCount = equippedWeapon.bulletCount;
        }

        bulletText.text = bulletCount + "/" + bulletCount;

        foreach (var visual in weaponVisuals)
        {
            visual.weaponObject.SetActive(false);
        }

        WeaponVisual match = weaponVisuals.Find(v => v.weaponData == newWeapon);
        if (match != null)
        {
            match.weaponObject.SetActive(true);
            bulletCount = newWeapon.bulletCount;
        }
        else
        {
            Debug.LogWarning($"No visual found for {newWeapon.name}");
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

    private void Shoot()
    {
        if (equippedWeapon == null || !canShoot || bulletCount <= 0)
            return;

        bulletCount--;

        bulletText.text = bulletCount + "/" + equippedWeapon.bulletCount;


        WeaponVisual currentVisual = weaponVisuals.Find(v =>
            v.weaponData == equippedWeapon && v.weaponObject.activeSelf);

        Transform muzzle = (currentVisual != null && currentVisual.muzzlePoint != null)
            ? currentVisual.muzzlePoint
            : transform;


        Plane plane = new Plane(Vector3.up, muzzle.position);
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(camRay, out float enter))
        {
            Vector3 targetPoint = camRay.GetPoint(enter);
            Vector3 direction = (targetPoint - muzzle.position).normalized;


            if (Physics.Raycast(muzzle.position, direction, out RaycastHit hit, 100f, ~0, QueryTriggerInteraction.Collide))
            {
                Debug.DrawLine(muzzle.position, hit.point, Color.red, 2f);
                Debug.Log($"Hit: {hit.collider.name}");

                EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(equippedWeapon.baseDamage, hit.point);
                }
            }
        }

        StartCoroutine(AttackSpeed());
    }


    IEnumerator AttackSpeed()
    {
        canShoot = false;
        yield return new WaitForSeconds(equippedWeapon.attackSpeed);
        canShoot = true;
    }

    IEnumerator Reload()
    {
        reloadingText.SetActive(true);
        yield return new WaitForSeconds(equippedWeapon.reloadSpeed);
        bulletCount = equippedWeapon.bulletCount;
        bulletText.text = bulletCount + "/" + bulletCount;
        reloadingText.SetActive(false);
    }
}

[System.Serializable]
public class WeaponVisual
{
    public WeaponItem weaponData;
    public GameObject weaponObject;
    public Transform muzzlePoint;
}
